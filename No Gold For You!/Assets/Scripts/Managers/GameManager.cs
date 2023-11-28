using System;
using UnityEngine;

public class GameManager : TManager<GameManager> {
    // Global Variables
    public int gravity;

    // Global referneces
    public Transform player { get; private set; }
    public ToolManager toolManager { get; private set; }

    public GameState currState { get; private set; }

    // Global events
    public static event Action<GameState> OnBeforeStateChange;
    public static event Action<GameState> OnAfterStateChange;
	
    void Start() {
        ChangeState(GameState.LoadingLevel);
        ChangeState(GameState.Initializing);
        ChangeState(GameState.Playing);
    }

    void Update() {
        
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
				break;
            case GameState.Playing:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.Paused:
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
            case GameState.Escaped:
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
    Escaped,
}