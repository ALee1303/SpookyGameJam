using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public GameObject VooDooPrefab;

    public UIController CurrentUI;

    public ScoreManager ScoreManager;

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
	}

    void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        if (newScene.name == "PinballBoardMain")
        {
            ScoreManager.OnScoreUpdate += HandleScoreUpdate;
            ScoreManager.OnMultiplierUpdate += HandleMultiUpdate;
            ScoreManager.OnPainUpdate += HandlePainUpdate;
        }
    }

	
    // TODO: talk to UI, etc
    void HandleScoreUpdate(int newScore)
    {
        BoardUI boardUI = (BoardUI)CurrentUI;
        if (boardUI)
            boardUI.UpdateScoreText(newScore);
    }

    void HandleMultiUpdate(float newMulti)
    {
        BoardUI boardUI = (BoardUI)CurrentUI;
        if (boardUI)
            boardUI.UpdateMultiplierText(newMulti);
    }

    void HandlePainUpdate(float newPain)
    {
        BoardUI boardUI = (BoardUI)CurrentUI;
        if (boardUI)
            boardUI.UpdatePainSlider(newPain);
        if (newPain >= 100.0f)
            OnPainFull();
    }

    // TODO: End game logic
    public void OnPainFull()
    {

    }
}
