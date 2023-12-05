using System;
using UnityEngine;

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
	
    void Start() {
        ChangeState(GameState.LoadingLevel);
        ChangeState(GameState.Initializing);
        ChangeState(GameState.Playing);
    }

    void Update() {
		if (currState != GameState.Playing) return;

		timer -= Time.deltaTime;

		if (timer <= 0) {
			ChangeState(GameState.TimeUp);
		}
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

                timer = 20; //TODO
				break;
            case GameState.Playing:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
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