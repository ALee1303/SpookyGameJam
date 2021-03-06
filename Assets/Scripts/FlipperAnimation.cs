﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlipperAnimation : MonoBehaviour {

    public KeyCode key = new KeyCode();
    AudioSource audioData;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key))
        {
            //transform.DOPlay();
            //gameObject.GetComponent(DOTweenAnimation).

            DOTween.PlayForward(gameObject,"flip");
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);

            print("key was pressed");
        }
        if (Input.GetKeyUp(key))
        {
            //transform.DOPlay();

            DOTween.PlayBackwards(gameObject, "flip");
            print("key was pressed");
        }


    }
}
