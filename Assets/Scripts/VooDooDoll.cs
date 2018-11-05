using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VooDooDoll : MonoBehaviour 
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if collided object is an obstacle
        ObstacleBase obstacle = collision.transform.GetComponent<ObstacleBase>();
        if (obstacle != null)
        {
            obstacle.Interact(collision.relativeVelocity.magnitude);
        }
    }
}
