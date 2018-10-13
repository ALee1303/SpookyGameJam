using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Playing, GameOver };

public class GameManager : Singleton<GameManager>
{

    public GameObject VooDooPrefab;

    public UIController CurrentUI;

    public ScoreManager ScoreManager;

    public GameState GameState { get; private set; }

    private int lives;

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();
        // TODO: move when title screen is available
        GameState = GameState.Playing;
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name != "PinballBoardMain")
            SceneManager.LoadScene("PinballBoardMain",LoadSceneMode.Additive);
    }


    public void OnDollDestroyed()
    {
        lives -= 1;
        BoardUI boardUI = (BoardUI)CurrentUI;
        if (boardUI)
            boardUI.UpdateLivesText(lives);
        if (lives == 0)
        {
            GameOver();
        }
    }

    // TODO: gameover logic, UI change, Restart, ETC
    void GameOver()
    {
        GameState = GameState.GameOver;
    }

    void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        if (newScene.name == "PinballBoardMain")
        {
            ScoreManager.OnScoreUpdate += HandleScoreUpdate;
            ScoreManager.OnMultiplierUpdate += HandleMultiUpdate;
        }
    }

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

}
