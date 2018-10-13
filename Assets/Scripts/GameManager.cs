using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {


    ScoreManager scoreManager;


	// Use this for initialization
	void Start ()
    {
        ScoreManager.Instance.OnScoreChanged += HandleScoreChanged;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // TODO: talk to UI, etc
    void HandleScoreChanged(int deltaScore)
    {

    }
}
