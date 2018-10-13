using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour, IInteractable<VooDooDoll>
{

    [SerializeField] float pullbackSpeed = 5f, launchSpeed = 50f;

    [SerializeField] float pullbackLimit = 0.3f, currentPosY;

    [SerializeField] Vector3 currentPos;

    // Use this for initialization
    void Start () {
        currentPosY = transform.position.y;
        currentPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = Vector3.Slerp(
                currentPos,
                new Vector3(currentPos.x, currentPos.y - pullbackLimit, currentPos.z),
                Time.deltaTime * pullbackSpeed);
        }
        if (!Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = Vector3.Lerp(
                currentPos,
                new Vector3(currentPos.x, currentPos.y, currentPos.z),
                Time.deltaTime * launchSpeed);
        }
	}

    public void Interact(VooDooDoll instigator)
    {
        
    }
}
