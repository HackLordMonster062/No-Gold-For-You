using TMPro;
using UnityEngine;

public class EscapeFailScreen : EndScreen {
	[SerializeField] TMP_Text destructionPerc;
	[SerializeField] TMP_Text bombsCount;

	public void SetDetails(float destruction, int bombs) {
		destructionPerc.text = (destruction * 100).ToString("0") + "%";
		bombsCount.text = bombs.ToString();
	}
}