using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public GameObject VooDooPrefab;

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake();
        ScoreManager.Instance.OnScoreUpdate = HandleScoreChanged;
        ScoreManager.Instance.OnPainUpdate = HandlePainChanged;
	}

    void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {

    }

	
    // TODO: talk to UI, etc
    void HandleScoreChanged(int newScore)
    {
        UIManager.Instance.UpdateScoreText(newScore);
    }

    void HandlePainChanged(float newPain)
    {
        UIManager.Instance.UpdatePainSlider(newPain);
        if (newPain >= 100.0f)
            OnPainFull();
    }

    // TODO: End game logic
    public void OnPainFull()
    {

    }
}
