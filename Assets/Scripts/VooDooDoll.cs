using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VooDooDoll : MonoBehaviour 
{

    float pain = 0.0f;
    public UnityAction<float> OnPainUpdate;

    public void UpdatePain(float deltaPain)
    {
        pain += deltaPain;
        Mathf.Clamp(pain, 0.0f, 100.0f);
        if (pain >= 100.0f)
            return;
        OnPainUpdate.Invoke(deltaPain);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // interact with VooDooDoll Interactable
        ObstacleBase obstacle = collision.transform.GetComponent<ObstacleBase>();
        if (obstacle != null)
        {
            obstacle.Interact(this);
            if (obstacle.Clip)
                SFXPlayer.Instance.PlayClip(obstacle.Clip, collision.relativeVelocity.magnitude);
        }
    }
}
