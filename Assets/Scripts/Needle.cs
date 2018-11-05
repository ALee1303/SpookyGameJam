using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    bool isStuck = false;

    [SerializeField]
    float multiplierChanger = 0.1f;

    [SerializeField]
    float mass = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStuck)
            return;
        // get voodoo doll
        VooDooDoll doll = collision.transform.GetComponent<VooDooDoll>();
        if (doll)
        {
            GameManager.Instance.ScoreManager.UpdateMultiplier(multiplierChanger);
            attachNeedle(doll.transform);
            isStuck = true;
        }
    }

    private void attachNeedle(Transform bodyPart)
    {
        // make needle's parent to get the whole needle to follow player
        this.transform.parent = bodyPart.transform;
        bodyPart.GetComponent<Rigidbody2D>().mass += mass;
        this.GetComponent<Collider2D>().enabled = false;
    }
}
