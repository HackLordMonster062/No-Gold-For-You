using UnityEngine;

public class ToolManager : MonoBehaviour {
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

		currTool = Tool.Pickaxe;
		_currToolRef = pickaxe;

        _cameraRef = Camera.main.transform;
        bombs = 5;
	}

    void Update() {
        if (GameManager.Instance.currState != GameState.Playing) return;

        switch (currTool) {
            case Tool.Pickaxe:
                if (Input.GetMouseButtonDown(0)) {
                    Mine();
                } else if (Input.GetMouseButtonDown(1) && bombs > 0) {
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
        Ray ray = new Ray(_cameraRef.position, _cameraRef.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, reach) && hitInfo.collider.CompareTag("MiningSpot")) {
            MiningSpot miningSpot = hitInfo.collider.GetComponent<MiningSpot>();

            if (miningSpot != null && miningSpot.Mine()) {
                ChangeTool(Tool.Gold);
            }
        }
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