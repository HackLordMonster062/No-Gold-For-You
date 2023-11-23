using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningSpot : MonoBehaviour {
    [SerializeField] int maxIntegrity;

    int _integrity;

	private void Awake() {
		_integrity = maxIntegrity;
	}

	public bool Mine() {
        _integrity--;

        if (_integrity == 0) {
            _integrity = maxIntegrity;
            return true;
        }

        return false;
    }
}
