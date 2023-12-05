using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExplosionManager : TManager<ExplosionManager> {
    public Vector3[] grid;
    public Transform map;
    public float voxelSize;

	public bool _showGizmos;

	void Start() {
		
    }

    void Update() {
		
    }

	public void Explode() {

	}

	private void OnDrawGizmos() {
		if (_showGizmos) {
			foreach (Vector3 vec in grid) {
				Gizmos.DrawSphere(vec, .2f);
			}
		}
	}

	public void LoadGridPreset(GridPreset preset) {
		if (preset != null) {
			grid = preset.gridPositions.Clone() as Vector3[];
		}
	}
}
