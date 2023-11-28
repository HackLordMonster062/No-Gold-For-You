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

	public override void Enter() {
		_neutralSM.currState.Enter();
	}

	public override Type Update() {
		_neutralSM.Update();

		if (_owner.IsPlayerInVision() && GameManager.Instance.toolManager.currTool == Tool.Bomb)
			return typeof(SuspiciousState);

		return null;
	}

	public override void Exit() {
		_neutralSM.currState.Exit();
	}
}