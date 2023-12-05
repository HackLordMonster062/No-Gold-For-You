using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridPreset", menuName = "GridPreset")]
public class GridPreset : ScriptableObject {
	public Vector2[] gridPositions;
}
