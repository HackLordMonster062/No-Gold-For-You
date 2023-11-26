using System;
using UnityEngine;

public class LookingForCrateState : TState {
	Transform _currCrate;

	public LookingForCrateState(MinerController owner) : base(owner) {
		_currCrate = GameObject.FindWithTag("Crate").transform;
	}

	public override void Enter() {
		foreach (GameObject crate in GameObject.FindGameObjectsWithTag("Crate")) {
			if (Vector3.Distance(crate.transform.position, _owner.transform.position) < Vector3.Distance(_currCrate.position, _owner.transform.position)) {
				_currCrate = crate.transform;
			}
		}

		_owner.navAgent.isStopped = false;
		_owner.navAgent.SetDestination(_currCrate.position);
		_owner.currCrate = _currCrate.GetComponent<Crate>();
	}

	public override Type Update() {
		if (_owner.navAgent.remainingDistance <= _owner.stoppingDistance) {
			return typeof(DumpingState);
		}

		return null;
	}

	public override void Exit() {
		_owner.navAgent.isStopped = true;
	}
}
