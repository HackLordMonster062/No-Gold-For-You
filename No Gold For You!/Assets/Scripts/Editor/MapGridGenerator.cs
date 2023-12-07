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
			string levelName = "Level1";

			string path = "Assets/Resources/GridPresets/" + levelName + "GridPreset.asset";
			GridPreset gridPreset = AssetDatabase.LoadAssetAtPath<GridPreset>(path);

			(target as ExplosionManager).LoadGridPreset(gridPreset);
		}
	}

	void GenerateGrid(ExplosionManager manager) {
		Mesh mesh = manager.map.GetComponent<MeshFilter>().sharedMesh;
		Bounds bounds = mesh.bounds;

		int xVoxels = Mathf.CeilToInt((bounds.max.x - bounds.min.x) / manager.voxelSize);
		int yVoxels = Mathf.CeilToInt((bounds.max.z - bounds.min.z) / manager.voxelSize);

		List<Vector2> listGrid = new List<Vector2>();

		for (int x = 0; x < xVoxels; x++) {
			for (int y = 0; y < yVoxels; y++) {
				Vector3 currVec = new Vector3(x, .1f, y);
				Vector3 worldPoint = manager.map.TransformPoint(currVec * manager.voxelSize + bounds.min);
				Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.z);
					
				// Check if the worldPoint is within the bounds of the mesh
				if (Physics.RaycastAll(worldPoint, Vector3.down).Length % 2 == 1) {
					listGrid.Add(worldPoint2d);
				}
			}
		}

		manager.grid = listGrid.ToArray();

		string levelName = LevelManager.Instance.currLevelInfo.levelName;

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
