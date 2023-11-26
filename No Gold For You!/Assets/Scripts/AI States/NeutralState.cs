using System;

public class NeutralState : TState {
	StateMachine _neutralSM;

	public NeutralState(MinerController owner) : base(owner) { 
		_neutralSM = new StateMachine();

		_neutralSM.AddState(new LookingForGoldState(owner));
		_neutralSM.AddState(new MiningState(owner));
		_neutralSM.AddState(new LookingForCrateState(owner));
		_neutralSM.AddState(new DumpingState(owner));
	}

	public override Type Update() {
		_neutralSM.Update();
		return null; // Suspicious state
	}
}