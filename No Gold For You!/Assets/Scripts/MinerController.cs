using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerController : MonoBehaviour {
    StateMachine _stateMachine;
	
    void Start() {
        GameManager.OnBeforeStateChange += Init;
    }

    void Init(GameState state) {
        if (state != GameState.Initializing) return;

		_stateMachine = new StateMachine();

		_stateMachine.AddState(new NeutralState(this));
	}

    void Update() {
        
    }
}
