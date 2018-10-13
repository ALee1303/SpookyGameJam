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

    bool isDestryoing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VooDooDoll doll = collision.GetComponent<VooDooDoll>();
        if (!doll)
            return;
        if (isDestryoing)
            return;
        StartCoroutine(DestroyDoll(doll));
    }

    IEnumerator DestroyDoll(VooDooDoll doll)
    {
        isDestryoing = true;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(doll);
        isDestryoing = false;
        GameManager.Instance.OnDollDestroyed();
        if (GameManager.Instance.GameState == GameState.Playing)
            Instantiate(dollPrefab, spawnPoint.position, Quaternion.identity);
    }
}
