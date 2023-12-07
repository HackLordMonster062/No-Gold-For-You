using UnityEngine;

public class ExplosionManager : TManager<ExplosionManager> {
    public float voxelSize;
	[SerializeField] float explosionRadius;

    public Transform map;

    public Vector2[] grid;
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
			Vector2 bombPos = new Vector2(bomb.transform.position.x, bomb.transform.position.z);

			for (int i = 0; i < grid.Length; i++) {
				if (!hasExploded[i] && Vector2.SqrMagnitude(grid[i] - bombPos) <= explosionRadius * explosionRadius) {
					exploded++;
					hasExploded[i] = true;
				}
			}
		}

		return (float)exploded / grid.Length;
	}

	private void OnDrawGizmos() {
		if (_showGizmos) {
			foreach (Vector2 vec in grid) {
				Gizmos.DrawSphere(new Vector3(vec.x, 0, vec.y), .2f);
			}
		}
	}

	public void LoadGridPreset(GridPreset preset) {
		if (preset != null) {
			grid = preset.gridPositions.Clone() as Vector2[];
		}
	}
}
