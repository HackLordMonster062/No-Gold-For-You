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

	protected override void Awake() {
        base.Awake();

	}

	void Start() {
        SceneManager.sceneLoaded += InitiateLevel;
        ChangeState(GameState.LoadingLevel);
    }

    void Update() {
		if (currState != GameState.Playing) return;

		timer -= Time.deltaTime;

        UIManager.Instance.UpdateHUD(timer, toolManager.bombs, SuspicionManager.Instance.suspicionLevelRaw);

		if (timer <= 0) {
			ChangeState(GameState.TimeUp);
		}
	}

    void InitiateLevel(Scene scene, LoadSceneMode mode) {
        if (LevelManager.Instance == null || scene.name.CompareTo(LevelManager.Instance.currLevelInfo.sceneName) != 0) return;

        ChangeState(GameState.Initializing);
        ChangeState(GameState.Playing);
    }

    public void ChangeState(GameState state) {
        OnBeforeStateChange?.Invoke(state);
        currState = state;

        print(currState);

        switch (state) {
            case GameState.LoadingLevel:
                LevelManager.Instance.LoadLevel();
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
				Debug.Log(ExplosionManager.Instance.Explode());
                break;
            case GameState.Escaped:
                ChangeState(GameState.LevelEnd);
                break;
            case GameState.TimeUp:
				ChangeState(GameState.LevelEnd);
                break;
            case GameState.Caught:
                ChangeState(GameState.LevelEnd);
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