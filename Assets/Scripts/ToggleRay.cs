using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// to be attached to the controller for which you want to toggle RayInteractor
/// switches between RayInteractor and DirectInteractor
/// </summary>
public class ToggleRay : MonoBehaviour
{
    public InputActionReference _joystickTouched;
 
    // define a public InputActionReference for toggle button
    // and a reference to the rayInteractor GameObject to be toggled
    public GameObject _rayInteractorController;
    // need a global variable for the XRDirectInteractor reference 
    public XRDirectInteractor _directInteractor;

    void Awake()
    {
        _joystickTouched.action.started += Pressed;
        _joystickTouched.action.canceled += Released;
        // add Pressed and Released events to action's .started and .canceled states
        // get a reference to the XRDirectInteractor attached to current gameObject
        _directInteractor = GetComponent<XRDirectInteractor>();
    }

    private void OnDestroy()
    {
        // remove event listeners when destroyed
    }

    void Pressed(InputAction.CallbackContext context)
    {
        // toggle the Ray 
        Toggle();
    }

    void Released(InputAction.CallbackContext context)
    {
        
        // toggle the Ray
        Toggle();
    }

    void Toggle()
    {
        // get a bool, isToggled, for the current state of the rayInteractor
        Debug.Log("Toggled");
        bool isToggled = _rayInteractorController.activeSelf;
        if (!isToggled) {
            _directInteractor.enabled = false; 
            _rayInteractorController.SetActive(true);
        } else {
            _rayInteractorController.SetActive(false);
            _directInteractor.enabled = true; 
        }
        // setActive of the rayInteractor based on the bool value
        // set enable of the directInteractor based on the bool value
    }
}
