using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private float multiplier = 1.0f;

    public UnityAction<int> OnScoreUpdate;
    public UnityAction<float> OnMultiplierUpdate;

    private void Awake()
    {
        GameManager.Instance.ScoreManager = this;
    }

    public void UpdateScore(float scoreModifier)
    {
        int deltascore = (int)(scoreModifier * multiplier);
        score += deltascore;
        if (score <= 0)
            score = 0;
        OnScoreUpdate.Invoke(score);
    }

    public void UpdateMultiplier(float deltaMultiplier)
    {
        multiplier += deltaMultiplier;
        OnMultiplierUpdate(multiplier);
    }

    public void HandleDollDestroyed()
    {
        multiplier = 1.0f;
        OnMultiplierUpdate(multiplier);
    }

    private void OnDestroy()
    {
        OnScoreUpdate = null;
        OnMultiplierUpdate = null;
    }
}