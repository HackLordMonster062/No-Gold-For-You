using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Info Objects/Level Info")]
public class LevelInfo : ScriptableObject {
	public string levelName;
	public string sceneName;
	public GridPreset gridPreset;
	public float startTime;
	public float startSuspicion;
}
