using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimateOnCollide : MonoBehaviour {
    [SerializeField]
    float scoreAdd = 100;
    // Use this for initialization
    void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        // get voodoo doll
        VooDooDoll doll = collision.transform.GetComponent<VooDooDoll>();
        if (doll)
        {
            //ScoreManager.Instance.UpdateScore(scoreAdd);
            DOTween.Rewind(gameObject);
            DOTween.PlayForward(gameObject);
        }
    }
}
