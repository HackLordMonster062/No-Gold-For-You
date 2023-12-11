using Mono.Cecil.Cil;
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
    [SerializeField] GameObject canvasPrefab;

    HUD _hudPanel;
	EscapeFailScreen _escapedFailPanel;
	SuccessScreen _escapedSuccessPanel;
	TimeUpScreen _timeUpPanel;
	CaughtScreen _caughtPanel;
	GameObject _pausePanel;

    Transform _canvas;

	void Start() {
        GameManager.OnBeforeStateChange += InitiateUI;
    }

    void Update() {
        
    }

    void InitiateUI(GameState state) {
        if (state != GameState.Initializing) return;

        if (_canvas ==  null) {
            _canvas = Instantiate(canvasPrefab).transform;
        }

        _hudPanel = Instantiate(hudPanelPrefab, _canvas).GetComponent<HUD>();

        _pausePanel = Instantiate(pausePanelPrefab, _canvas);

        _pausePanel.SetActive(false);
    }

    public void UpdateHUD(float time, int bombs, float suspicion) {
        int minutes = (int)time / 60;
        float seconds = time % 60;

        _hudPanel.UpdateHUD(minutes.ToString() + ":" + seconds.ToString("00.00"), bombs.ToString(), suspicion);
    }

    public void SetPause(bool pause) {
        _pausePanel.SetActive(pause);
    }

    public void Caught(float destruction, int bombs) {
        _caughtPanel = Instantiate(caughtPanelPrefab, _canvas).GetComponent<CaughtScreen>();

        _caughtPanel.SetDetails(destruction, bombs);
    }

    public void TimeUp(float destruction, int bombs) {
        _timeUpPanel = Instantiate(timeUpPanelPrefab, _canvas).GetComponent<TimeUpScreen>();

        _timeUpPanel.SetDetails(destruction, bombs);
	}

    public void EscapeSuccess(float destruction, int bombs, float time) {
        _escapedSuccessPanel = Instantiate(escapedSuccessPanelPrefab, _canvas).GetComponent<SuccessScreen>();

        _escapedSuccessPanel.SetDetails(destruction, bombs, time);
	}

    public void EscapeFail(float destruction, int bombs) {
        _escapedFailPanel = Instantiate(escapedFailPanelPrefab, _canvas).GetComponent<EscapeFailScreen>();

        _escapedFailPanel.SetDetails(destruction, bombs);
	}
}
