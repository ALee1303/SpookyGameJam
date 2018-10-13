using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

    Transform pupilTrans, vdTrans;

    CircleCollider2D iris, pupilBox;

    float radius, distance;

    Vector2 centerPos;

	// Use this for initialization
	void Start () {
        pupilTrans = GameObject.FindGameObjectWithTag("Pupil").
            transform;
        iris = GetComponent<CircleCollider2D>();
        pupilBox = GameObject.FindGameObjectWithTag("Pupil").
            GetComponent<CircleCollider2D>();
        vdTrans = GameObject.FindGameObjectWithTag("Voodoo").transform;
        radius = iris.radius;
        centerPos = iris.offset;
	}
	
	// Update is called once per frame
	void Update () {
        print(radius);

        pupilTrans.position = Vector3.Slerp(
            pupilTrans.position,
            vdTrans.position,
            Time.deltaTime
            );

        distance = Vector2.Distance(pupilTrans.position, centerPos);

        //pupilTrans.position = Vector3.Slerp(
        //    pupilTrans.position,
        //    fpTrans.position,
        //    Time.deltaTime * 5
        //    );

        if (distance > radius)
        {
            Vector2 fromOriginToObject = new Vector2(pupilTrans.position.x, pupilTrans.position.y) - centerPos;

            fromOriginToObject *= radius / distance;

            pupilTrans.position = centerPos + fromOriginToObject;
        }
	}
}
