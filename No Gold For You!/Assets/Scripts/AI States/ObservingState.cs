using System;
using UnityEngine;

public class ObservingState : TState {


	public ObservingState(MinerController owner) : base(owner) { }

	public override Type Update() {
		if (Vector3.Distance(_owner.transform.position, GameManager.Instance.player.position) >= _owner.followingDistance)
			return typeof(FollowingState);

		return null;
	}
}
