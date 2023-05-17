using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIncoming : MonoBehaviour
{
    public Transform spawn;
    public Transform despawn;
    public float travelTime = 10f;

    public void SpawnObject(GameObject objectSpawned) {
        GameObject spawned = Instantiate(objectSpawned, spawn.position, spawn.rotation);
        StartCoroutine(SendObjectToDespawn(spawned));
    }
    IEnumerator SendObjectToDespawn(GameObject spawned) {
        float t = 0;
        while(t < 1)
        {
            spawned.transform.position = Vector3.Lerp(spawn.position,despawn.position,t);
            t = t + Time.deltaTime / travelTime;
            yield return new WaitForEndOfFrame();
        }
        spawned.transform.position = despawn.position;
    }
}
