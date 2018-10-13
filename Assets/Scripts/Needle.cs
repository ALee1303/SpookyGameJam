using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour, IInteractable<VooDooDoll> 
{
    [SerializeField]
    Collider2D halfCollider;
    [SerializeField]
    Collider2D fullCollider;

    [SerializeField]
    float multiplierChanger = 0.1f;

    void Awake()
    {
        halfCollider.enabled = true;
        fullCollider.enabled = false;
        // added to score manager
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        VooDooDoll doll = col.transform.GetComponent<VooDooDoll>();
        //exit if collision wasnt with VooDoo
        if (!doll)
            return;
        // make needle follow player
        this.transform.parent = doll.transform;
        //change collider
        halfCollider.enabled = false;
        fullCollider.enabled = true;
    }

    public void Interact(VooDooDoll instigator)
    {
        ScoreManager.Instance.UpdateMultiplier(multiplierChanger);
    }
}
