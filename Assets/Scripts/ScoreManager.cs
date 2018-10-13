using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    private int score;
    private float multiplier;

    public UnityAction<int> OnScoreChanged;

    protected override void Awake()
    {
        base.Awake();
        score = 0;
        multiplier = 1.0f;
    }

    public void AddScore(float point)
    {
        int deltascore = (int)(point * multiplier);
        score += deltascore;
        OnScoreChanged.Invoke(deltascore);
    }

    public void ReduceScore(float point)
    {
        int deltascore = (int)(point * multiplier);
        score -= deltascore;
        if (score <= 0)
            score = 0;
        OnScoreChanged.Invoke(-deltascore);
    }

    public void UpdateMultiplier(float deltaMultiplier)
    {
        multiplier += deltaMultiplier;
    }
}