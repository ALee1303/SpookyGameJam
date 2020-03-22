using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour {

    BoxCollider2D boxCol;

    [SerializeField] float flipperSpeedUp = 20f, flipperSpeedDown = 20f;

	// Use this for initialization
	void Start () {
        boxCol = GetComponentInChildren<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.tag == "LeftFlipper")
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.Euler(new Vector3(0, 0, 90)),
                    Time.deltaTime * flipperSpeedUp
                    );
        }
        if (!Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.tag == "LeftFlipper")
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    Quaternion.Euler(new Vector3(0, 0, 0)),
                    Time.deltaTime * flipperSpeedDown
                    );
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.tag == "RightFlipper")
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.Euler(new Vector3(0, 0, -90)),
                    Time.deltaTime * flipperSpeedUp
                    );
        }
        if (!Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.tag == "RightFlipper")
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.Euler(new Vector3(0, 0, 0)),
                    Time.deltaTime * flipperSpeedDown
                    );
        }
    }
}
