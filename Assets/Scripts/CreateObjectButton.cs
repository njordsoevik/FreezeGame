using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CreateObjectButton : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Transform spawnTransform;

    public void CreateObjectOnSpawn() {
        GameObject g = Instantiate(prefabToInstantiate, spawnTransform.position, spawnTransform.rotation);
    }


}
