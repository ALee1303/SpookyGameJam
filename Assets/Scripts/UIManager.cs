using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> 
{
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text multiplierText;

    [SerializeField]
    Slider painSlider;

    public void UpdateScoreText(int newScore)
    {
        //
        scoreText.text = "SCORE: " + newScore.ToString();
    }

    public void UpdatePainSlider(float deltaPain)
    {
        painSlider.value += deltaPain;
    }
}
