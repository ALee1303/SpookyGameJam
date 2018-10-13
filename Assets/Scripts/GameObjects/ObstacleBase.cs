using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour, IInteractable<VooDooDoll> 
{
    [SerializeField]
    protected float scoreModifier;

    [SerializeField]
    protected AudioClip clip;

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // play clip based on velocity
        SFXPlayer.Instance.PlayClip(clip, collision.relativeVelocity.magnitude);
    }

    public virtual void Interact(VooDooDoll instigator)
    {
        ScoreManager.Instance.UpdateScore(scoreModifier);
    }
}
