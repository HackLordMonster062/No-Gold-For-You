using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Info Objects/Level Info")]
public class LevelInfo : ScriptableObject {
	[Header("Info")]
	public string levelName;
	public string sceneName;
	public GridPreset gridPreset;
	[Header("Starting Values")]
	public float startTime;
	public float startSuspicion;
	public int startBombs;
	[Header("Requirements")]
	public float requiredTime;
	public float requiredBombs;
	public float requiredDestruction;
}
