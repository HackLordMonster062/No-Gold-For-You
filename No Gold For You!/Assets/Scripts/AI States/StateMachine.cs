using System;
using System.Collections.Generic;
using UnityEngine;

public class TState {
	protected MinerController _owner;

	public TState(MinerController owner) {
		_owner = owner;
	}

	public virtual void Enter() { }

	public virtual Type Update() { return null; }

	public virtual void Exit() { }
}

public class StateMachine {
	public TState currState { get; private set; }
	public Dictionary<Type, TState> statePool;

	Type _nextState;

	public StateMachine() {
		statePool = new Dictionary<Type, TState>();
	}

	public void ChangeState(Type newState) {
		if (currState != null) {
			currState.Exit();
		}

		currState = statePool[newState];

		currState.Enter();
	}

	public void Update() {
		if (currState != null) {
			_nextState = currState.Update();

			if (_nextState != null) {
				ChangeState(_nextState);
			}
		}
	}

	public void AddState(TState newState) {
		statePool[newState.GetType()] = newState;

		if (currState == null) {
			ChangeState(newState.GetType());
		}
	}
}
