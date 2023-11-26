using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionManager : TManager<SuspicionManager> {
	public int SuspicionLevel { get; private set; }

    float _suspicionLevelRaw;
	
    void Start() {
        
    }

    void Update() {
        _suspicionLevelRaw = Mathf.Clamp(_suspicionLevelRaw, 0, 3);
        SuspicionLevel = (int)_suspicionLevelRaw;
    }
}
