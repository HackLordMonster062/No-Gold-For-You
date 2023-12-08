using System;
using UnityEngine.SceneManagement;

public class LevelManager : TManager<LevelManager> {
	public LevelInfo currLevelInfo {  get; private set; }
    public LevelInfo[] levels;

    int _currLevelIndex;

    void Update() {
        
    }

    public LevelInfo LoadInfoNoLoad() {
		currLevelInfo = levels[_currLevelIndex];

		return currLevelInfo;
    }

    public LevelInfo LoadLevel(int index) {
        _currLevelIndex = index;

        currLevelInfo = levels[index];

        SceneManager.LoadScene(currLevelInfo.sceneName);

        return currLevelInfo;
    }

    public LevelInfo LoadLevel() {
        return LoadLevel(_currLevelIndex);
    }

    public LevelInfo LoadLevel(string levelName) {
        for (int i = 0; i < levels.Length; i++) {
            if (levels[i].levelName.CompareTo(levelName) == 0) {
                return LoadLevel(i);
            }
        }

        return null;
    }
}
