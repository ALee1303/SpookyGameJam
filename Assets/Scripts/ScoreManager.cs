using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    private int score;
    private float multiplier;

    public UnityAction<int> OnScoreUpdate;

    protected override void Awake()
    {
        base.Awake();
        score = 0;
        multiplier = 1.0f;
    }

    public void UpdateScore(float scoreModifier)
    {
        int deltascore = (int)(scoreModifier * multiplier);
        score += deltascore;
        if (score <= 0)
            score = 0;
        OnScoreUpdate.Invoke(deltascore);
    }

    public void UpdateMultiplier(float deltaMultiplier)
    {
        multiplier += deltaMultiplier;
    }
}