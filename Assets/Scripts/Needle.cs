using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{

    [SerializeField]
    float multiplierChanger = 0.1f;

    [SerializeField]
    float mass = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // get voodoo doll
        VooDooDoll doll = collision.transform.GetComponent<VooDooDoll>();
        if (doll)
        {
            ScoreManager.Instance.UpdateMultiplier(multiplierChanger);
            attachNeedle(doll.transform);
        }
    }

    private void attachNeedle(Transform bodyPart)
    {
        // make needle follow player
        this.transform.parent.parent = bodyPart.transform;
        bodyPart.GetComponent<Rigidbody2D>().mass += mass;
        this.GetComponent<Collider2D>().enabled = false;
    }
}
