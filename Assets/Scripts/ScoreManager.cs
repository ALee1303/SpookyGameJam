using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    private int score;

    public UnityAction<int> OnScoreChanged;

    public void AddScore(int point)
    {
        score += point;
        OnScoreChanged.Invoke(point);
    }

    public void ReduceScore(int point)
    {
        score -= point;
        if (score <= 0)
            score = 0;
        OnScoreChanged.Invoke(-point);
    }
}