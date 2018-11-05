using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField]
    protected float scoreModifier;
    
    [SerializeField]
    protected AudioClip clip;

    /// <summary>
    /// called be VooDooDoll when colliding
    /// </summary>
    /// <param name="velocity">magnitude of the collision velocity</param>
    public virtual void Interact(float velocity)
    {
        // add the score
        GameManager.Instance.ScoreManager.UpdateScore(scoreModifier);
        //play the clip
        if (clip)
            SFXPlayer.Instance.PlayClip(clip, velocity);
    }
}
