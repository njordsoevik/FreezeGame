using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DuplicateObject : MonoBehaviour
{
    public InputActionReference _actionReference;

    void Awake()
    {
        // add Cloned and Detached events to action's .started and .canceled states
        _actionReference.action.started += Cloned;
        _actionReference.action.canceled += Detached;
    }

    void Start()
    {
        
    }

    private void Cloned(InputAction.CallbackContext context)
    {
        // if the object is selected
        // instantiate a copy of the current gameObject in the current position/rotation
        if (GetComponent<XRGrabInteractable>().isSelected) {
            Color currentColor = gameObject.GetComponent<HighlightGrabObject>().GetOGColor();
            GameObject clonedObject = Instantiate(gameObject, transform.position, transform.rotation);
            clonedObject.GetComponent<Rigidbody>().isKinematic = false;
            clonedObject.GetComponent<Rigidbody>().useGravity = false;
            clonedObject.GetComponent<Collider>().isTrigger = false;
            if (clonedObject.GetComponent<MeshRenderer>()) {
                clonedObject.GetComponent<MeshRenderer>().material.color = currentColor;
            }

        }

    }

    private void Detached(InputAction.CallbackContext context)
    {
        // can specify custom behaviors for the original object when detached
    }
}
