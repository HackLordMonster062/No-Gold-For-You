using System;
using UnityEngine;

public class Crate : MonoBehaviour {
	public static event Action OnGoldInCrate;

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("PlayerGold"))
			OnGoldInCrate?.Invoke();
	}
}
