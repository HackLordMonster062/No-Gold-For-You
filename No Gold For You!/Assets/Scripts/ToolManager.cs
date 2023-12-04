using System;
using UnityEngine;

public class ToolManager : MonoBehaviour {
    public static event Action OnPlayerMined;
    public static event Action OnPlayerDumped;
    public static event Action OnPlayerDroppedBomb;

    public int bombs { get; private set; }
	public Tool currTool { get; private set; }
    Transform _currToolRef;

    [SerializeField] float reach;
    [SerializeField] float throwingForce;

    [SerializeField] Transform pickaxe;
    [SerializeField] Transform bomb;
    [SerializeField] Transform gold;

    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject goldPrefab;

    Transform _cameraRef;
	
    void Awake() {
        GameManager.OnBeforeStateChange += Init;
	}

    void Init(GameState state) {
        if (state != GameState.Initializing) return;

        ChangeTool(Tool.Pickaxe);

        _cameraRef = Camera.main.transform;
        bombs = 5;
	}

    void Update() {
        if (GameManager.Instance.currState != GameState.Playing) return;

        switch (currTool) {
            case Tool.Pickaxe:
                if (Input.GetMouseButtonDown(0)) {
                    Mine();

                    OnPlayerMined?.Invoke();
                } else if (Input.GetMouseButtonDown(1) && bombs > 0) {
                    ChangeTool(Tool.Bomb);
                }

                break;
            case Tool.Bomb:
				if (Input.GetMouseButtonDown(1)) {
					ThrowBomb();

					if (bombs == 0)
						ChangeTool(Tool.Pickaxe);

                    OnPlayerDroppedBomb?.Invoke();
				} else if (Input.GetMouseButtonDown(0)) {
                    ChangeTool(Tool.Pickaxe);
				}

				break;
            case Tool.Gold:
                if (Input.GetMouseButtonDown(0)) {
                    OnPlayerDumped?.Invoke();
                    ThrowGold();
					ChangeTool(Tool.Pickaxe);
				} else if (Input.GetMouseButtonDown(1)) {
                    OnPlayerDumped?.Invoke();
                    ThrowGold();
                    if (bombs > 0)
						ChangeTool(Tool.Bomb);
					else
						ChangeTool(Tool.Pickaxe);
				}

				break;
        }
    }

    void ChangeTool(Tool tool) {
        currTool = tool;

        if (_currToolRef != null)
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
        Ray ray = new Ray(_cameraRef.position, _cameraRef.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, reach) && hitInfo.collider.CompareTag("MiningSpot")) {
            MiningSpot miningSpot = hitInfo.collider.GetComponent<MiningSpot>();

            if (miningSpot != null && miningSpot.Mine()) {
                print(miningSpot);
                ChangeTool(Tool.Gold);
            }
        }
    }

    void ThrowBomb() {
        Rigidbody bombRB = Instantiate(bombPrefab, bomb.position, bomb.rotation).GetComponent<Rigidbody>();
        bombRB.AddForce(bomb.forward * throwingForce);

        bombs--;
    }

    void ThrowGold() {
		Rigidbody goldRB = Instantiate(goldPrefab, gold.position, gold.rotation).GetComponent<Rigidbody>();
		goldRB.AddForce(gold.forward * throwingForce);
	}
}

public enum Tool {
    Pickaxe,
    Bomb,
    Gold
}