using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour, IInteractable<VooDooDoll>
{

    [SerializeField] float pullbackSpeed = 5f, launchSpeed = 50f;

    [SerializeField] float pullbackLimit = 0.3f;

    [SerializeField] Rigidbody2D rb;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localScale = Vector3.Slerp(
                transform.localScale,
                new Vector3(1, pullbackLimit, 1),
                Time.deltaTime * pullbackSpeed);
        }
        if (!Input.GetKey(KeyCode.DownArrow))
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                new Vector3(1, 1, 1),
                Time.deltaTime * launchSpeed);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.up * 2000);
        }
	}

    public void Interact(VooDooDoll instigator)
    {
        
    }
}
