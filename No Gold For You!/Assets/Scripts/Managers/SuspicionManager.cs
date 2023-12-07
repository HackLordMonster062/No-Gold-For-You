using UnityEngine;

public class SuspicionManager : TManager<SuspicionManager> {
    [SerializeField] float suspicionRate;
    [SerializeField] float suspicionFadingRate;
    [SerializeField] float suspicionPenaltyAmount;

	public int suspicionLevel { get; private set; }
    public int suspectingMiners { get; set; }

    float _suspicionLevelRaw;
	
    void Start() {
        
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

    public void Penalty(float amount) {
        _suspicionLevelRaw += amount;
    }

    public void Penalty() {
        _suspicionLevelRaw += suspicionPenaltyAmount;
    }
}
