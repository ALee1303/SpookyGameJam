using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    GameObject dollPrefab;

    [SerializeField]
    AudioClip clip;

    [SerializeField]
    float destroyDelay;

    [SerializeField]
    SmoothCamera2D mainCamera;

    [SerializeField]
    EyeLook eye;

    bool isDestryoing;

    /// <summary>
    /// Destroys the doll when the doll enters the Trigger Parameter
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        VooDooDoll doll = collision.GetComponent<VooDooDoll>();
        if (!doll)
            return;
        if (isDestryoing)
            return;
        GetComponent<AudioSource>().Play();
        StartCoroutine(DestroyDoll());
        // if the game didn't end from previous line, respawn the doll
        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            Transform newDoll = Instantiate(dollPrefab, spawnPoint.position, Quaternion.identity).transform.GetChild(0);
            mainCamera.target = newDoll;
            eye.Doll = newDoll;
        }
    }

    /// <summary>
    /// Coroutine for Destroying the doll with a delay.
    /// Handles all the doll dependency set on GameManager after destruction
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyDoll()
    {
        isDestryoing = true;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(GameObject.FindWithTag("Voodoo"));
        isDestryoing = false;
        // handle GameOver logic
        GameManager.Instance.OnDollDestroyed();
    }
}