using System;
using UnityEngine;

public class DumpingState : TState {
	
	
    public DumpingState(MinerController owner) : base(owner) { }

	public override Type Update() {
		return typeof(LookingForGoldState);
	}
}
