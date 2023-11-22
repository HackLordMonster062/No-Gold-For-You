using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour {
	public Tool currTool { get; private set; }
    Transform _currToolRef;

    [SerializeField] Transform pickaxe;
    [SerializeField] Transform bomb;
    [SerializeField] Transform gold;

    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject goldPrefab;
	
    void Awake() {
        GameManager.Instance.OnBeforeStateChange += Init;
	}

    void Init(GameState state) {
        if (state != GameState.Initializing) return;

		currTool = Tool.Pickaxe;
		_currToolRef = pickaxe;
	}

    void Update() {
        if (GameManager.Instance.currState != GameState.Playing) return;

        switch (currTool) {
            case Tool.Pickaxe:
                if (Input.GetMouseButtonDown(0)) {
                    Mine();
                } else if (Input.GetMouseButtonDown(1)) {
                    currTool = Tool.Bomb;
                }

                break;
            case Tool.Bomb:
				if (Input.GetMouseButtonDown(1)) {
					ThrowBomb();
				} else if (Input.GetMouseButtonDown(0)) {
					currTool = Tool.Pickaxe;
				}

				break;
            case Tool.Gold:
                if (Input.GetMouseButtonDown(0)) {
                    ThrowGold();
                    currTool = Tool.Pickaxe;
                } else if (Input.GetMouseButtonDown(1)) {
                    ThrowGold();
                    currTool = Tool.Bomb;
                }

				break;
        }
    }

    void ChangeTool(Tool tool) {
        currTool = tool;

        _currToolRef.gameObject.SetActive(false);

        switch (tool) {
            case Tool.Pickaxe:
                _currToolRef = pickaxe;
                break;
			case Tool.Bomb:
				_currToolRef = bomb;
				break;
			case Tool.Gold:
				_currToolRef = gold;
				break;
		}
        
        _currToolRef.gameObject.SetActive(true);
    }

    void Mine() {

    }

    void ThrowBomb() {

    }

    void ThrowGold() {

    }
}

public enum Tool {
    Pickaxe,
    Bomb,
    Gold
}