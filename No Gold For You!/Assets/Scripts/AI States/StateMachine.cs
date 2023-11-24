using UnityEngine;

public class TState {
	MinerController _owner;

	public TState(MinerController owner) {
		_owner = owner;
	}

	public virtual void Enter() { }

	public virtual TState Update() { return null; }

	public virtual void Exit() { }
}

public class StateMachine {
	public TState currState { get; private set; }

	TState _nextState;

	public void ChangeState(TState newState) {
		if (currState != null) {
			currState.Exit();
		}

		currState = newState;

		currState.Enter();
	}

	public void Update() {
		if (currState != null) {
			_nextState = currState.Update();

			if (_nextState != null ) {
				ChangeState(_nextState);
			}
		}
	}
}
