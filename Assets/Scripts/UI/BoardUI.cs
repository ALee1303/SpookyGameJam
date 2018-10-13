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
}
