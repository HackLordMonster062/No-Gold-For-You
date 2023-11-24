using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerController : MonoBehaviour {


    StateMachine _stateMachine;

    public NeutralState neutralState { get; private set; }
    public LookingForGoldState lookingForGoldState { get; private set; }
    public MiningState miningState { get; private set; }
    public LookingForCrateState lookingForCrateState { get; private set; }
    public DumpingState dumpingState { get; private set; }

	
    void Start() {
        GameManager.OnBeforeStateChange += Init;
    }

    void Init(GameState state) {
        if (state != GameState.Initializing) return;

        neutralState = new NeutralState(this);
		lookingForGoldState = new LookingForGoldState(this);
		miningState = new MiningState(this);
		lookingForCrateState = new LookingForCrateState(this);
		dumpingState = new DumpingState(this);

		_stateMachine = new StateMachine(neutralState);
	}

    void Update() {
        
    }
}
