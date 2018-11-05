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

    /// <summary>
    /// called by obstacles when they collide with the doll
    /// </summary>
    /// <param name="scoreModifier">score each obstacles are worth</param>
    public void UpdateScore(float scoreModifier)
    {
        int deltascore = (int)(scoreModifier * multiplier);
        score += deltascore;
        if (score <= 0)
            score = 0;
        OnScoreUpdate.Invoke(score);
    }

    /// <summary>
    /// called by needle when colliding with the doll
    /// </summary>
    /// <param name="deltaMultiplier">multiplier of the attaached needle added to the player</param>
    public void UpdateMultiplier(float deltaMultiplier)
    {
        multiplier += deltaMultiplier;
        OnMultiplierUpdate.Invoke(multiplier);
    }

    /// <summary>
    /// Called by GameManager when dolls are destroyed
    /// </summary>
    public void HandleDollDestroyed()
    {
        multiplier = 1.0f;
        OnMultiplierUpdate.Invoke(multiplier);
    }

    private void OnDestroy()
    {
        OnScoreUpdate = null;
        OnMultiplierUpdate = null;
    }
}