using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : TManager<GameManager> {
    // Global Variables
    public int gravity;

    // Global referneces
    public Transform player { get; private set; }
    public ToolManager toolManager { get; private set; }

    public GameState currState { get; private set; }
    public float timer { get; private set; }

    // Global events
    public static event Action<GameState> OnBeforeStateChange;
    public static event Action<GameState> OnAfterStateChange;

    float _destructionPerc;
    int _bombsUsed;
    float _timeTaken;

	protected override void Awake() {
        base.Awake();

	}

	void Start() {
        SceneManager.sceneLoaded += InitiateLevel;
        StartCurrLevel();
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause(currState == GameState.Playing);
        }

		if (currState != GameState.Playing) return;

		timer -= Time.deltaTime;

        UIManager.Instance.UpdateHUD(timer, toolManager.bombs, SuspicionManager.Instance.suspicionLevelRaw);

		if (timer <= 0) {
			ChangeState(GameState.TimeUp);
		}
	}

    public void StartCurrLevel() {
		LevelManager.Instance.LoadLevel();
		ChangeState(GameState.LoadingLevel);
	}

    void InitiateLevel(Scene scene, LoadSceneMode mode) {
        if (LevelManager.Instance == null || scene.name.CompareTo(LevelManager.Instance.currLevelInfo.sceneName) != 0) return;

        ChangeState(GameState.Initializing);
        ChangeState(GameState.Playing);
    }

    void CalculateScores() {
		_destructionPerc = ExplosionManager.Instance.Explode();
        _bombsUsed = LevelManager.Instance.currLevelInfo.startBombs - toolManager.bombs;
        _timeTaken = LevelManager.Instance.currLevelInfo.startTime - timer;
	}

    public void TogglePause(bool pause) {
        if (!pause) {
			ChangeState(GameState.Playing);
		} else {
            ChangeState(GameState.Paused);
        }

        UIManager.Instance.SetPause(pause);
    }

    public void ChangeState(GameState state) {
        OnBeforeStateChange?.Invoke(state);
        currState = state;

        switch (state) {
            case GameState.LoadingLevel:
                break;
            case GameState.Initializing:
				player = GameObject.FindGameObjectWithTag("Player").transform;
				toolManager = player.GetComponent<ToolManager>();

                timer = LevelManager.Instance.currLevelInfo.startTime;
				break;
            case GameState.Playing:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                UIManager.Instance.SetPause(false);
                break;
            case GameState.Paused:
                Time.timeScale = 0;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

                UIManager.Instance.SetPause(true);
				break;
            case GameState.LevelEnd:
                Time.timeScale = 0;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
                CalculateScores();
                break;
            case GameState.Escaped:
                ChangeState(GameState.LevelEnd);

                if (_destructionPerc >= LevelManager.Instance.currLevelInfo.requiredDestruction)
                    UIManager.Instance.EscapeSuccess(_destructionPerc, _bombsUsed, _timeTaken);
                else
                    UIManager.Instance.EscapeFail(_destructionPerc, _bombsUsed);
                break;
            case GameState.TimeUp:
				ChangeState(GameState.LevelEnd);
                UIManager.Instance.TimeUp(_destructionPerc, _bombsUsed);
                break;
            case GameState.Caught:
                ChangeState(GameState.LevelEnd);
                UIManager.Instance.Caught(_destructionPerc, _bombsUsed);
                break;
        }

        OnAfterStateChange?.Invoke(state);
    }
}

public enum GameState {
    LoadingLevel,
    Initializing,
    Playing,
    Paused,
    LevelEnd,
    Escaped,
    TimeUp,
    Caught,
}