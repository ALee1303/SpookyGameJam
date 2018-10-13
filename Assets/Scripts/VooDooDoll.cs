using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VooDooDoll : MonoBehaviour 
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // interact with VooDooDoll Interactable
        ObstacleBase obstacle = collision.transform.GetComponent<ObstacleBase>();
        if (obstacle != null)
        {
            obstacle.Interact(this);
            SFXPlayer.Instance.PlayClip(obstacle.Clip, collision.relativeVelocity.magnitude);
        }
    }
}
