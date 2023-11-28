using System;

public class SuspiciousState : TState {
	StateMachine _suspiciousSM;

	bool _playerMining;
	bool _playerDumped;

	public SuspiciousState(MinerController owner) : base(owner) {
		_suspiciousSM = new StateMachine();

		_suspiciousSM.AddState(new FollowingState(owner));
		_suspiciousSM.AddState(new ObservingState(owner));

		ToolManager.OnPlayerMined += () => _playerMining = true;
		ToolManager.OnPlayerDumped += () => _playerDumped = true;
	}

	public override void Enter() {
		SuspicionManager.Instance.suspectingMiners++;

		_playerMining = false;
		_playerDumped = false;

		_suspiciousSM.currState.Enter();
	}

	public override Type Update() {
		_suspiciousSM.Update();

		if (_owner.IsPlayerInVision()) {
			switch (SuspicionManager.Instance.suspicionLevel) {
				case 0:
					if (GameManager.Instance.toolManager.currTool == Tool.Pickaxe)
						return typeof(NeutralState);
					break;
				case 1:
					if (_playerMining)
						return typeof(NeutralState);
					break;
				case 2:
					if (_playerDumped)
						return typeof(NeutralState);
					break;
				case 3:
					break;
			}
		}

		return null;
	}

	public override void Exit() {
		SuspicionManager.Instance.suspectingMiners--;

		_suspiciousSM.currState.Exit();
	}
}