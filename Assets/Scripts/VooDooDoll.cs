using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VooDooDoll : MonoBehaviour 
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // interact with VooDooDoll Interactable
        IInteractable<VooDooDoll> dollInteract = collision.transform.GetComponent<IInteractable<VooDooDoll>>();
        if (dollInteract != null)
        {
            dollInteract.Interact(this);
        }
    }
}
