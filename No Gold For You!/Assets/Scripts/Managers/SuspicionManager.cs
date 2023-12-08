using UnityEngine;

public class SuspicionManager : TManager<SuspicionManager> {
    [SerializeField] float suspicionRate;
    [SerializeField] float suspicionFadingRate;
    [SerializeField] float suspicionPenaltyAmount;

    public int suspectingMiners { get; set; }

	public int suspicionLevel { get; private set; }
    public float suspicionLevelRaw { get; private set; }
	
    protected override void Awake() {
        base.Awake();

        GameManager.OnBeforeStateChange += InitiateSuspicion;
    }

    void Update() {
        if (GameManager.Instance.currState != GameState.Playing) return;

        suspicionLevelRaw -= suspicionFadingRate * Time.deltaTime;
        suspicionLevelRaw += suspicionRate * suspectingMiners * Time.deltaTime;

        suspicionLevelRaw = Mathf.Clamp(suspicionLevelRaw, 0, 3);
        suspicionLevel = (int)suspicionLevelRaw;

        if (suspicionLevel == 3) {
            GameManager.Instance.ChangeState(GameState.Caught);
        }
    }

    void InitiateSuspicion(GameState state) {
        if (state != GameState.Initializing) return;

        suspicionLevelRaw = LevelManager.Instance.currLevelInfo.startSuspicion;
    }

    public void Penalty(float amount) {
        suspicionLevelRaw += amount;
    }

    public void Penalty() {
        suspicionLevelRaw += suspicionPenaltyAmount;
    }
}
