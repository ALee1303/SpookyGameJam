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
    Slider painSlider;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "SCORE: " + newScore.ToString(); 
    }
    public void UpdatePainSlider(float newPain)
    {
        painSlider.value = newPain;
    }
}
