using System;
using UnityEngine;

public class MiningState : TState {
	public MiningState(MinerController owner) : base(owner) {
	}

	public override Type Update() {
		return typeof(LookingForCrateState);
	}
}
