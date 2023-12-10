using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	
	
    public void Resume() {
		GameManager.Instance.TogglePause(false);
	}

	public void Restart() {
		GameManager.Instance.StartCurrLevel();
	}

	public void Exit() {
		// TODO
	}
}
