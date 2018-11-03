using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum GameState { Title, Playing, GameOver };

public class GameManager : Singleton<GameManager>
{
    public UnityAction OnGameOver;

    public GameObject VooDooPrefab;

    public UIController CurrentUI;

    public ScoreManager ScoreManager;

    Scene currentScene;

    [SerializeField]
    float restartDelay = 3.0f;

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

    public void ChangeState(GameState newState)
    {
        // must change Property
        GameState = newState;
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
                lives = 3;
                HandleLoadScene("PinballBoardMain");
                break;
            case GameState.GameOver:
                OnGameOver.Invoke();
                StartCoroutine(RestartGame());
                break;
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(restartDelay);
        ChangeState(GameState.Title);
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
            // calls StateChanged which calls HandleGameOver
            ChangeState(GameState.GameOver);
        }
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

    protected override void OnDestroy()
    {
        OnGameOver = null;
        base.OnDestroy();
    }
}
