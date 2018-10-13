using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField]
    protected float scoreModifier;

    //played by VooDoo
    [SerializeField]
    protected AudioClip clip;
    // accessor for voodoo
    public AudioClip Clip{ get { return clip; }}

    public virtual void Interact(VooDooDoll instigator)
    {
        ScoreManager.Instance.UpdateScore(scoreModifier);
    }
}
