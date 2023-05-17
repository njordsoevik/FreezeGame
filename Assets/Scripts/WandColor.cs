using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WandColor : MonoBehaviour
{
    public int currentColorIndex = 0;
    public List<Color> highlightColors = new List<Color> {
        Color.black,
        Color.blue,
        Color.red
    };
    public InputActionReference _leftActionReference;
    public InputActionReference _rightActionReference;

    MeshRenderer _renderer;
    GameObject tip;

    AudioSource _audioSource;

    void Awake()
    {
        _leftActionReference.action.started += ChangeColor;
        _rightActionReference.action.started += ChangeColor;
        tip = transform.GetChild(1).gameObject;

        _renderer = tip.GetComponent<MeshRenderer>();
        _renderer.material.color = highlightColors[currentColorIndex];

        _audioSource = tip.GetComponent<AudioSource>();
    }

    public Color GetOGColor() {
        return highlightColors[currentColorIndex];
    }

    private void ChangeColor(InputAction.CallbackContext context)
    {
        // if the object is selected
        // instantiate a copy of the current gameObject in the current position/rotation
        if (GetComponent<XRGrabInteractable>().isSelected) {
            currentColorIndex = (currentColorIndex + 1) % highlightColors.Count;
            _renderer.material.color = highlightColors[currentColorIndex];
            
            _audioSource.Play();
        }
        // can specify custom behaviors for the clone here
        // can do additional things like playing an sfx
    }
}
