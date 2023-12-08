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
	
    void Awake() {
        GameManager.OnBeforeStateChange += Init;
    }

	private void OnDestroy() {
		GameManager.OnAfterStateChange -= Init;
	}

	void Init(GameState state) {
        if (state != GameState.Initializing) return;

		navAgent = GetComponent<NavMeshAgent>();

		_stateMachine = new StateMachine();

		_stateMachine.AddState(new NeutralState(this));
        _stateMachine.AddState(new SuspiciousState(this));
	}

    void Update() {
		if (GameManager.Instance.currState != GameState.Playing) return;

		_stateMachine?.Update();
    }

     public bool IsPlayerInVision() {
        Vector3 distanceVector = GameManager.Instance.player.position - transform.position;

		return distanceVector.magnitude <= visionDistance && Vector3.Angle(distanceVector, transform.forward) <= visionAngle;
	}
}
