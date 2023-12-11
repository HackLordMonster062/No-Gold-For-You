using TMPro;
using UnityEngine;

public class SuccessScreen : EndScreen {
	[SerializeField] TMP_Text destructionPerc;
	[SerializeField] TMP_Text bombsCount;
	[SerializeField] TMP_Text timeTaken;

	public void SetDetails(float destruction, int bombs, float time) {
		destructionPerc.text = (destruction * 100).ToString("0") + "%";
		bombsCount.text = bombs.ToString();
		timeTaken.text = (time / 60).ToString("0") + ":" + (time % 60).ToString("00");
	}
}