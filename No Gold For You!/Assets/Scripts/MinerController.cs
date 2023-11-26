using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MinerController : MonoBehaviour {
    public NavMeshAgent navAgent { get; private set; }
    public MiningSpot currMiningSpot { get; set; }
    public Crate currCrate { get; set; }

    public float stoppingDistance;
    public float followingDistance;

    StateMachine _stateMachine;
	
    void Start() {
        GameManager.OnBeforeStateChange += Init;
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Init(GameState state) {
        if (state != GameState.Initializing) return;

		_stateMachine = new StateMachine();

		_stateMachine.AddState(new NeutralState(this));
	}

    void Update() {
        _stateMachine?.Update();
    }
}
