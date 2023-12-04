using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExplosionManager))]
public class MapGridGenerator : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if (GUILayout.Button("Generate Grid")) {
			GenerateGrid(target as ExplosionManager);
		}

		if (GUILayout.Button("Load Default File")) {
			string levelName = "Level1"; // TODO: make it use the levelmanager level name

			string path = "Assets/Resources/GridPresets/" + levelName + "GridPreset.asset";
			GridPreset gridPreset = AssetDatabase.LoadAssetAtPath<GridPreset>(path);

			(target as ExplosionManager).LoadGridPreset(gridPreset);
		}
	}

	void GenerateGrid(ExplosionManager manager) {
		Mesh mesh = manager.map.GetComponent<MeshFilter>().sharedMesh;
		Bounds bounds = mesh.bounds;

		int xVoxels = Mathf.CeilToInt((bounds.max.x - bounds.min.x) / manager.voxelSize);
		int yVoxels = Mathf.CeilToInt((bounds.max.y - bounds.min.y) / manager.voxelSize);
		int zVoxels = Mathf.CeilToInt((bounds.max.z - bounds.min.z) / manager.voxelSize);

		List<Vector3> listGrid = new List<Vector3>();

		for (int x = 0; x < xVoxels; x++) {
			for (int y = 0; y < yVoxels; y++) {
				for (int z = 0; z < zVoxels; z++) {
					Vector3 currVec = new Vector3(x, y, z);
					Vector3 worldPoint = manager.map.TransformPoint(currVec * manager.voxelSize + bounds.min);
					
					// Check if the worldPoint is within the bounds of the mesh
					if (Physics.RaycastAll(worldPoint, Vector3.down).Length % 2 == 1) {
						listGrid.Add(worldPoint);
					}
				}
			}
		}

		manager.grid = listGrid.ToArray();

		string levelName = "Level1"; // TODO: make it use the levelmanager level name

		string path = "Assets/Resources/GridPresets/" + levelName + "GridPreset.asset";

		GridPreset gridPreset = AssetDatabase.LoadAssetAtPath<GridPreset>(path);

		if (gridPreset == null) {
			gridPreset = ScriptableObject.CreateInstance<GridPreset>();
			AssetDatabase.CreateAsset(gridPreset, path);
		}

		gridPreset.gridPositions = manager.grid;

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
}
