using System;

public class SuspiciousState : TState {
	StateMachine _suspiciousSM;

	public SuspiciousState(MinerController owner) : base(owner) {
		_suspiciousSM = new StateMachine();

		_suspiciousSM.AddState(new LookingForGoldState(owner));
		_suspiciousSM.AddState(new MiningState(owner));
	}

	public override Type Update() {
		_suspiciousSM.Update();
		return null; // Suspicious state
	}
}