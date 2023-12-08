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
	GameObject _escapedFailPanel;
	GameObject _escapedSuccessPanel;
	GameObject _timeUpPanel;
	GameObject _caughtPanel;
	GameObject _pausePanel;

    Transform _canvas;

	void Start() {
        
    }

    void Update() {
        
    }

    void InitiateUI() {
        _canvas = FindObjectOfType<Canvas>().transform;

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

        _hudPanel.UpdateHUD(minutes.ToString() + ":" + seconds.ToString("F1"), bombs.ToString(), suspicion);
    }

    public void SetPause(bool pause) {
        _pausePanel.SetActive(pause);
    }

    public void Caught() {
        _caughtPanel = Instantiate(caughtPanelPrefab, _canvas);
    }

    public void TimeUp() {
        _timeUpPanel = Instantiate(timeUpPanelPrefab, _canvas);
    }

    public void EscapeSuccess() {
        _escapedSuccessPanel = Instantiate(escapedSuccessPanelPrefab, _canvas);
    }

    public void EscapeFail() {
        _escapedFailPanel = Instantiate(escapedFailPanelPrefab, _canvas);
    }
}
