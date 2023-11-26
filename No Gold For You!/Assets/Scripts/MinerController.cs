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

    [Header("Navigation")]
    public float stoppingDistance;
    public float followingDistance;

    [Header("Vision")]
    [SerializeField] float visionAngle;
    [SerializeField] float visionDistance;

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
		if (GameManager.Instance.currState != GameState.Playing) return;

        if (IsPlayerInVision())
            CheckPlayer();

		_stateMachine?.Update();
    }

    void CheckPlayer() {
        switch (SuspicionManager.Instance.SuspicionLevel) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    bool IsPlayerInVision() {
        return Vector3.Distance(GameManager.Instance.player.position, transform.position) <= visionDistance && Vector3.Angle(GameManager.Instance.player.position - transform.position, transform.forward) <= visionAngle;
	}
}
