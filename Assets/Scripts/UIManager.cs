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

    [SerializeField]
    GameObject MainUI;

    [SerializeField]
    GameObject GameUI;

    public void SpawnUI(string currentScene)
    {
        if (currentScene == "MainMenu")
        {
            Instantiate(MainUI);
        }
        if (currentScene == "PinballBoardMain")
        {
            Instantiate(GameUI);
        }
    }

    public void UpdateScoreText(int newScore)
    {
        //
        scoreText.text = "SCORE: " + newScore.ToString();
    }

    public void UpdatePainSlider(float newPain)
    {
        painSlider.value = newPain;
    }
}
