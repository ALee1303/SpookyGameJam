using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour {

    [SerializeField]
    GameObject doll;

    public Vector2 origin;

    public float eyeRadius = 3.0f;

    public float viewRange = 10.0f;

    public float speed = 10.0f;

    private void Awake()
    {
        origin = this.transform.position;
    }

    void FixedUpdate()
    {
        Vector2 targetLoc;
        Vector2 direction = (Vector2)doll.transform.position - origin;
        if (direction.magnitude < eyeRadius)
        {
            targetLoc = doll.transform.position;
        }
        else
        {
            Vector2 displacement = Vector2.ClampMagnitude(direction, eyeRadius);
            targetLoc = origin + displacement;
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, targetLoc, speed * Time.deltaTime);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, eyeRadius);
        Gizmos.DrawWireSphere(this.transform.position, viewRange);
    }
}
