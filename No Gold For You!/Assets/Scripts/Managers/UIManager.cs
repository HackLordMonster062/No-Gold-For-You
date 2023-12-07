using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : TManager<UIManager> {
    [SerializeField] GameObject hudPanelPrefab;
    [SerializeField] GameObject escapedFailPanelPrefab;
    [SerializeField] GameObject escapedSuccessPanelPrefab;
    [SerializeField] GameObject timeUpPanelPrefab;
    [SerializeField] GameObject caughtPanelPrefab;
    [SerializeField] GameObject pausePanelPrefab;

    HUD _hudPanel;
	GameObject _escapedFailPanel;
	GameObject _escapedSuccessPanel;
	GameObject _timeUpPanel;
	GameObject _caughtPanel;
	GameObject _pausePanel;

	void Start() {
        
    }

    void Update() {
        
    }

    void InitiateUI() {
        _hudPanel = Instantiate(hudPanelPrefab).GetComponent<HUD>();
        _hudPanel.suspicionBar.maxValue = 3;

        _pausePanel = Instantiate(pausePanelPrefab);

        _pausePanel.SetActive(false);
    }

    public void UpdateHUD(float time, int bombs, float suspicion) {
        _hudPanel.bombs.text = bombs.ToString();

        int minutes = (int)time / 60;
        float seconds = time % 60;

        _hudPanel.timer.text = minutes.ToString() + ":" + seconds.ToString("F1");

        _hudPanel.suspicionBar.value = suspicion;
    }

    public void SetPause(bool pause) {
        _pausePanel.SetActive(pause);
    }

    public void Caught() {
        _caughtPanel = Instantiate(caughtPanelPrefab);
    }

    public void TimeUp() {
        _timeUpPanel = Instantiate(timeUpPanelPrefab);
    }

    public void EscapeSuccess() {
        _escapedSuccessPanel = Instantiate(escapedSuccessPanelPrefab);
    }

    public void EscapeFail() {
        _escapedFailPanel = Instantiate(escapedFailPanelPrefab);
    }
}
