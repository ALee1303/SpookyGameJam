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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VooDooDoll doll = collision.GetComponent<VooDooDoll>();
        if (!doll)
            return;
        if (isDestryoing)
            return;
        StartCoroutine(DestroyDoll());
    }

    IEnumerator DestroyDoll()
    {
        isDestryoing = true;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(GameObject.FindWithTag("Voodoo"));
        isDestryoing = false;
        GameManager.Instance.OnDollDestroyed();
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            Transform newDoll = Instantiate(dollPrefab, spawnPoint.position, Quaternion.identity).transform.GetChild(0);
            mainCamera.target = newDoll;
            eye.doll = newDoll;
        }
    }
}