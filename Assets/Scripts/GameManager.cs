using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum GameState { Title, Playing, GameOver };

public class GameManager : Singleton<GameManager>
{
   // Called on GameOver
    public UnityAction OnGameOver;
    
    // Reference to current UI, set on Awake call of the corresponding UIController used
    public UIController CurrentUI;

    // Reference to ScoreManager. Only Set when Playing and BoardScene is loaded on ScoreManager's Awake()
    public ScoreManager ScoreManager;

    // current state loaded. used to Unload Scene on scene transition
    private Scene currentScene;

    // time between GameOver and Title screen
    [SerializeField]
    private float restartDelay = 3.0f;

    // Lives before GameOver
    [SerializeField]
    private int lives = 3;

    // current GameState
    [SerializeField]
    private GameState currentState;

    // Property for current Gamestate.
    // Use this to set GameState instead of gameState to properly handle stateChanged logic
    public GameState CurrentState
    {
        get { return currentState; }
        private set 
        {
            if (currentState != value)
            {
                currentState = value;
                StateChanged(value);
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        // must change Property
        CurrentState = newState;
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
        // load the scene when first starting the gmae
        StateChanged(CurrentState);
    }

    void LoadScene(string newScene)
    {
        // unload previous scene
        if (currentScene.name != null)
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        // in case scene already loaded but not set as currentScence
        Scene duplicate = SceneManager.GetSceneByName(newScene);
        // load this scene only if this scene was not loaded
        if (duplicate.name != newScene)
            SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
    }

    void StateChanged(GameState newState)
    {
        switch(newState)
        {
            case GameState.Title:
                LoadScene("Title");
                break;
            case GameState.Playing:
                lives = 3;
                LoadScene("PinballBoardMain");
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

    // Initializzes ScoreManager
    void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        currentScene = newScene;
        if (newScene.name == "PinballBoardMain")
        {
            ScoreManager.OnScoreUpdate += HandleScoreUpdate;
            ScoreManager.OnMultiplierUpdate += HandleMultiUpdate;
            OnGameOver += ((BoardUI)CurrentUI).UpdateGameOverText;
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
