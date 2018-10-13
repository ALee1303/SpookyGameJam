using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable<VooDooDoll> 
{
    void OnCollisionEnter2D(Collision2D col)
    {
    }

    public void Interact(VooDooDoll instigator)
    {

    }
}
