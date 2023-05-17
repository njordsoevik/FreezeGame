using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnHit : MonoBehaviour
{
    //AudioSource audioSource;
    public float EXPLOSION_FORCE = 500f;
    public float EXPLOSION_RADIUS = 1f;
    public float EXPLOSION_UPWARDS_MODIFIER = 0f;
    public float EXPLOSION_TIME = 0.2f;
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        Rigidbody[] childRigidbodys = gameObject.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childRigidbody in childRigidbodys) {
            childRigidbody.isKinematic = false;
            childRigidbody.AddExplosionForce(EXPLOSION_FORCE, transform.position, EXPLOSION_RADIUS, EXPLOSION_UPWARDS_MODIFIER);
        }
        StartCoroutine(DisableObjectAfterTime());
    }

    void OnCollisionEnter(Collision collision)
    {
            
    }

    IEnumerator DisableObjectAfterTime() {
        yield return new WaitForSeconds(EXPLOSION_TIME);
        Destroy(gameObject);
    }
}
