using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ExplosionManager : TManager<ExplosionManager> {
    public float voxelSize;
	[SerializeField] float explosionRadius;

    public Transform map;

    public Vector3[] grid;
	public bool _showGizmos;

	void Start() {
		
    }

    void Update() {
		
    }

	public float Explode() {
		GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
		bool[] hasExploded = new bool[grid.Length];
		int exploded = 0;

		foreach (GameObject bomb in bombs) {
			for (int i = 0; i < grid.Length; i++) {
				if (!hasExploded[i] && Vector3.SqrMagnitude(grid[i] - bomb.transform.position) <= explosionRadius * explosionRadius) {
					exploded++;
					hasExploded[i] = true;
				}
			}
		}

		return (float)exploded / grid.Length;
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
