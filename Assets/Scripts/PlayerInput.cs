using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    Flipper leftFlipperScript, rightFlipperScript;

    Transform leftFlipperTrans, rightFlipperTrans;

	// Use this for initialization
	void Start () {
        leftFlipperScript = GameObject.FindGameObjectWithTag("LeftFlipper").
            GetComponent<Flipper>();
        leftFlipperTrans = GameObject.FindGameObjectWithTag("LeftFlipper").
            GetComponent<Transform>();
        rightFlipperTrans = GameObject.FindGameObjectWithTag("RightFlipper").
            GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //print("running");
        //if (Input.GetKey(KeyCode.LeftArrow))
        //    //leftFlipperScript.RotateFlipperUp(1, 0.5f);
        //    //Quaternion.Lerp(leftFlipperTrans.)
        //if (!Input.GetKey(KeyCode.LeftArrow))
        //    leftFlipperScript.RotateFlipperDown(1, 0.5f);
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    transform.rotation = Quaternion.identity;
        //    leftFlipperScript.RotateFlipper(10);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    leftFlipperTrans.Rotate(new Vector3(0, 0, -90));
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    rightFlipperTrans.Rotate(new Vector3(0, 0, -90));
        //}
        //if (Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    rightFlipperTrans.Rotate(new Vector3(0, 0, 90));
        //}
    }
}
