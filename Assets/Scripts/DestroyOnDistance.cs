using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDistance : MonoBehaviour
{
public float maxDistance = 10f;
private Vector3 instantiationPoint;

void Awake()
{
    instantiationPoint = transform.position;
}

// Update is called once per frame
void Update()
{
    float distance = Vector3.Distance(transform.position, instantiationPoint);

    if (distance > maxDistance)
    {
        DestroyObject script = GetComponent<DestroyObject>();
        StartCoroutine(script.Fade());
    }
}
}
