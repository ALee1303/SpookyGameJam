using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour {

    // reference of the doll to follow
    public Transform Doll;
    
    // boundary of the eye movement
    [SerializeField]
    private float eyeRadius = 3.0f;

    // how faast the eye follows the doll
    [SerializeField]
    private float speed = 10.0f;

    // center of the eye
    private Vector2 origin;

    private void Awake()
    {
        // set the origin to the middle
        origin = this.transform.position;
    }

    void FixedUpdate()
    {
        // point to move towards during this frame
        Vector2 targetLoc;
        // make sure theres a doll to follow
        if (Doll != null)
        {
            // gets the direction and distance between the doll and the eye
            Vector2 direction = (Vector2)Doll.transform.position - origin;
            // if doll is inside the radius of an eye, put the eyeball directly below the doll
            if (direction.magnitude < eyeRadius)
            {
                targetLoc = Doll.transform.position;
            }
            // if doll is outside of the boundary of an eye
            else
            {
                // clamp the displacement to the edge of the eye radius
                Vector2 displacement = Vector2.ClampMagnitude(direction, eyeRadius);
                // transform the target location
                targetLoc = origin + displacement;
            }
            // start moving the eye towards the target location
            this.transform.position = Vector2.MoveTowards(this.transform.position, targetLoc, speed * Time.deltaTime);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, eyeRadius);
    }
}
