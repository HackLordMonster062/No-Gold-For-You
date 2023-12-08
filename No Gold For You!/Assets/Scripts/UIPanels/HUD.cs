using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	[SerializeField] TMP_Text bombsCount;
	[SerializeField] TMP_Text timer;
	[SerializeField] Slider suspicionBar;

	[SerializeField] Color color0;
	[SerializeField] Color color1;
	[SerializeField] Color color2;

	Image _suspicionFill;

	private void OnEnable() {
		_suspicionFill = suspicionBar.fillRect.GetComponent<Image>();
		suspicionBar.maxValue = 3;
	}

	public void UpdateHUD(string time, string bombs, float suspicion) {
		timer.text = time;
		bombsCount.text = bombs;
		suspicionBar.value = suspicion;

		if (suspicion <= 1) {
			_suspicionFill.color = color0;
		} else if (suspicion <= 2) {
			_suspicionFill.color = color1;
		} else {
			_suspicionFill.color = color2;
		}
	}
}
