using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private float multiplier = 1.0f;
    private float pain = 0.0f;

    public UnityAction<int> OnScoreUpdate;
    public UnityAction<float> OnPainUpdate;

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
    }

    public void UpdatePain(float deltaPain)
    {
        pain += deltaPain;
        Mathf.Clamp(pain, 0.0f, 100.0f);
        if (pain >= 100.0f)
            return;
        OnPainUpdate.Invoke(pain);
    }
}