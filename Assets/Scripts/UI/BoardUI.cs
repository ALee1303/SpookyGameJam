using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : UIController {

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text multiplierText;

    [SerializeField]
    Text livesText;

    [SerializeField]
    Text gameOverText;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.OnGameOver += HandleGameOver;
    }


    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Tears: " + newScore.ToString(); 
    }
    public void UpdateMultiplierText(float newMult)
    {
        multiplierText.text = "Multiplier: x" + newMult.ToString("0.0");
    }
    public void UpdateLivesText(int newLives)
    {
        livesText.text = "x" + newLives.ToString();
    }

    public void HandleGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
            GameManager.Instance.OnGameOver -= HandleGameOver;
    }
}
