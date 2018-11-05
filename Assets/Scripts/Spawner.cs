using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    GameObject dollPrefab;

    // played on death
    [SerializeField]
    AudioClip clip;

    // time before destruction of the doll
    [SerializeField]
    float destroyDelay;

    // reference to set the camera to follow the respawned doll
    [SerializeField]
    SmoothCamera2D mainCamera;

    // reference to set the eye to follow the respawned doll
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
        // if the game didn't end from previous line, respawn the doll
        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            Transform newDoll = Instantiate(dollPrefab, spawnPoint.position, Quaternion.identity).transform.GetChild(0);
            mainCamera.target = newDoll;
            eye.Doll = newDoll;
        }
    }
}