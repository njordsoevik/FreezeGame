using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightGrabObject : MonoBehaviour
{
    MeshRenderer _interactor; 

    public InputActionReference _actionReference;

    AudioSource _audio;
    MeshRenderer _renderer;
    Color originalColor;
    bool isHighlighted = false;

    void Start()
    {
        // access and store the originalColor
        _renderer = GetComponent<MeshRenderer>();
        _interactor = GameObject.FindGameObjectWithTag("ControllerPoint").GetComponent<MeshRenderer>();
        originalColor = _renderer.material.color;
        _audio = GetComponent<AudioSource>();
        _actionReference.action.started += PermanentHighlight;
    }

    void Highlight()
    {
        // set isHighlighted true
        isHighlighted = true;
        // change the material color to highlightColor
        _renderer.material.color = _interactor.material.color;
    }

    void Dehighlight()
    {
        // set isHighlighted false
        isHighlighted = false;
        // change the material color to originalColor
        _renderer.material.color = originalColor;
    }

    public void ToggleHighlight()
    {
        
        // if not already highlighted, highlight the object
        if (!isHighlighted) {
            Highlight();
        } else { // else dehighlight it
            Dehighlight();
        }
    }

    public void PermanentHighlight(InputAction.CallbackContext context)
    {
        if (isHighlighted) {
            originalColor = _interactor.material.color;
            _audio.Play();
        }
    }

    public Color GetOGColor() {
        return originalColor;
    }
}

