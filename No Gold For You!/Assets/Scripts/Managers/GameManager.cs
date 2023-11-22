using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TManager<GameManager> {
    // Global Variables
    public int gravity;

    // Global referneces
    public Transform player { get; private set; }

    public GameState currState;

    // Global events
    public event Action<GameState> OnBeforeStateChange;
    public event Action<GameState> OnAfterStateChange;
	
    void Start() {
        ChangeState(GameState.LoadingLevel);
        ChangeState(GameState.Initializing);
    }

    void Update() {
        
    }

    public void ChangeState(GameState state) {
        OnBeforeStateChange(state);
        currState = state;

        switch (state) {
            case GameState.LoadingLevel:
                break;
            case GameState.Initializing:
				player = GameObject.FindGameObjectWithTag("Player").transform;
				break;
            case GameState.Playing: 
                break;
            case GameState.Paused:
                break;
            case GameState.Escaped:
                break;
        }

        OnAfterStateChange(state);
    }
}

public enum GameState {
    LoadingLevel,
    Initializing,
    Playing,
    Paused,
    Escaped,
}