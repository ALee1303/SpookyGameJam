using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Title, Playing, GameOver };

public class GameManager : Singleton<GameManager>
{

    public GameObject VooDooPrefab;

    public UIController CurrentUI;

    public ScoreManager ScoreManager;

    Scene currentScene;

    private int lives = 3;

    public GameState gameState;

    public GameState GameState
    {
        get { return gameState; }
        private set 
        {
            if (gameState != value)
            {
                gameState = value;
                StateChanged(GameState);
            }
        }
    }

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();
	}

    private void OnEnable()
    {
        // TODO: Title Screen
        SceneManager.sceneLoaded += OnSceneLoaded;
        HandleLoadScene("Title");
    }

    void HandleLoadScene(string newScene)
    {
        if (currentScene.name != null)
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
    }

    void StateChanged(GameState newState)
    {
        switch(newState)
        {
            case GameState.Title:
                HandleLoadScene("Title");
                break;
            case GameState.Playing:
                HandleLoadScene("PinballBoardMain");
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void OnDollDestroyed()
    {
        lives -= 1;
        BoardUI boardUI = (BoardUI)CurrentUI;
        ScoreManager.HandleDollDestroyed();
        if (boardUI)
            boardUI.UpdateLivesText(lives);
        if (lives == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameState = GameState.GameOver;

    }

    void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        currentScene = newScene;
        if (newScene.name == "Title")
        {

        }
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
