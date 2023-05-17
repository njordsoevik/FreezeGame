using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyObject : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip destroySound;
    public InputActionReference _actionReference;

    ParticleSystem _particles;

    void Awake()
    {
        // add Cloned and Detached events to action's .started and .canceled states
        _actionReference.action.started += Destroyed;

        _audioSource = GetComponent<AudioSource>();
        _particles = GetComponent<ParticleSystem>();
    }


    private void Destroyed(InputAction.CallbackContext context)
    {
        // if the object is selected
        // instantiate a copy of the current gameObject in the current position/rotation
        if (GetComponent<XRGrabInteractable>().isSelected) {
            
            StartCoroutine(Fade());
            
            //GetComponent<XRGrabInteractable>().colliders.Clear();
            
        }
    }

    public IEnumerator Fade()
    {
        _audioSource.PlayOneShot(destroySound, 1.0F);
        _particles.Play();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}
