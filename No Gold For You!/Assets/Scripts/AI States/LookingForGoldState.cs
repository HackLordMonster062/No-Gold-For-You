using System;
using UnityEngine;

public class LookingForGoldState : TState {
	Transform _currMiningSpot;

	public LookingForGoldState(MinerController owner) : base(owner) {
		_currMiningSpot = GameObject.FindWithTag("MiningSpot").transform;
	}

	public override void Enter() {
		foreach (GameObject miningSpot in GameObject.FindGameObjectsWithTag("MiningSpot")) {
			if (Vector3.Distance(miningSpot.transform.position, _owner.transform.position) < Vector3.Distance(_currMiningSpot.position, _owner.transform.position)) {
				_currMiningSpot = miningSpot.transform;
			}
		}

		_owner.navAgent.isStopped = false;
		_owner.navAgent.SetDestination(_currMiningSpot.position);
		_owner.currMiningSpot = _currMiningSpot.GetComponent<MiningSpot>();
	}

	public override Type Update() {
		if (_owner.navAgent.remainingDistance <= _owner.stoppingDistance) {
			return typeof(MiningState);
		}

		return null;
	}

	public override void Exit() {
		_owner.navAgent.isStopped = true;
	}
}
