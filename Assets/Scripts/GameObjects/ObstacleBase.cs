using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    [SerializeField]
    protected float scoreModifier;

    [SerializeField]
    protected AudioClip clip;

    public AudioClip Clip{ get { return clip; }}

    public virtual void Interact(VooDooDoll instigator)
    {
        ScoreManager.Instance.UpdateScore(scoreModifier);
    }
}
