using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public AudioSource _audioSource;
    public Collider _wandTipCollider;
    public GameObject _wandTip;
    // Start is called before the first frame update

    void OnCollisionEnter(Collision other) {
        // Debug.Log("Other collider "+other.collider +" Other object " + other.gameObject.name);
        // Debug.Log("want collider "+_wandTipCollider);
        if (!gameObject.GetComponent<Collider>().isTrigger) { // If trigger, let trigger enter handle effects
            _audioSource.Play();
            if (other.collider == _wandTipCollider) {
                Color otherColor = _wandTip.GetComponent<MeshRenderer>().material.color;
                gameObject.GetComponent<MeshRenderer>().material.color = otherColor;
                gameObject.GetComponent<HighlightObject>().SetOGColor(otherColor);
            }
        }

    }

    void OnTriggerEnter(Collider other) {
        _audioSource.Play();
        if (other == _wandTipCollider) {
            Color otherColor = _wandTip.GetComponent<MeshRenderer>().material.color;
            gameObject.GetComponent<MeshRenderer>().material.color = otherColor;
            gameObject.GetComponent<HighlightObject>().SetOGColor(otherColor);
        }
    }
}
