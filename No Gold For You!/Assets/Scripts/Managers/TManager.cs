using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TManager<T> : MonoBehaviour where T : MonoBehaviour {
	public static T Instance { get; private set; }
	
    void Awake() {
        if (Instance == null) {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        
    }
}
