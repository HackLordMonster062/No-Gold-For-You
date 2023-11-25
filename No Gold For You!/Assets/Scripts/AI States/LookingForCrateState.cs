using System;
using UnityEngine;

public class LookingForCrateState : TState {
	
    public LookingForCrateState(MinerController owner) : base(owner) {
	}

	public override Type Update() {
		return typeof(DumpingState);
	}
}
