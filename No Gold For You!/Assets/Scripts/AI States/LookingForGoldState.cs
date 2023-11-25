using System;

public class LookingForGoldState : TState {
	MiningSpot _currMiningSpot;

	public LookingForGoldState(MinerController owner) : base(owner) {
	}

	public override void Enter() {
		
	}

	public override Type Update() {
		return typeof(MiningState);
	}
}
