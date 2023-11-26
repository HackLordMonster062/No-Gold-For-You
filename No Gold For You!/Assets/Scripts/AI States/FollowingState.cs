using System;
using UnityEngine;

public class FollowingState : TState {


	public FollowingState(MinerController owner) : base(owner) { }

	public override void Enter() {
		_owner.navAgent.isStopped = false;
	}

	public override Type Update() {
		if (Vector3.Distance(_owner.transform.position, GameManager.Instance.player.position) >= _owner.followingDistance) {
			_owner.navAgent.SetDestination(GameManager.Instance.player.position);
			return null;
		}

		return typeof(ObservingState);
	}

	public override void Exit() {
		_owner.navAgent.isStopped = true;
	}
}
