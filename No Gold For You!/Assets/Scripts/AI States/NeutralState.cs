

public class NeutralState : TState {
	StateMachine _neutralSM;

	public NeutralState(MinerController owner) : base(owner) { 
		_neutralSM = new StateMachine(owner.lookingForGoldState);
	}


}