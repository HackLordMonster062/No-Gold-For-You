using UnityEngine;

public class SuspicionManager : TManager<SuspicionManager> {
    [SerializeField] float suspicionRate;
    [SerializeField] float suspicionFadingRate;
    [SerializeField] float suspicionPenaltyAmount;

	public int suspicionLevel { get; private set; }
    public int suspectingMiners { get; set; }

    float _suspicionLevelRaw;
	
    protected override void Awake() {
        base.Awake();

        GameManager.OnBeforeStateChange += InitiateSuspicion;
    }

    void Update() {
        if (GameManager.Instance.currState != GameState.Playing) return;

        _suspicionLevelRaw -= suspicionFadingRate * Time.deltaTime;
        _suspicionLevelRaw += suspicionRate * suspectingMiners * Time.deltaTime;

        _suspicionLevelRaw = Mathf.Clamp(_suspicionLevelRaw, 0, 3);
        suspicionLevel = (int)_suspicionLevelRaw;

        if (suspicionLevel == 3) {
            GameManager.Instance.ChangeState(GameState.Caught);
        }
    }

    void InitiateSuspicion(GameState state) {
        if (state != GameState.Initializing) return;

        _suspicionLevelRaw = LevelManager.Instance.currLevelInfo.startSuspicion;
    }

    public void Penalty(float amount) {
        _suspicionLevelRaw += amount;
    }

    public void Penalty() {
        _suspicionLevelRaw += suspicionPenaltyAmount;
    }
}
