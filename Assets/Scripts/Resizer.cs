using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Resizer : MonoBehaviour
{
    int MAX_SCALE = 1000;
    Vector3 initialScale;

    public InputActionReference inputReference = null;
    // Start is called before the first frame update

    // Update is called once per frame

    private void Awake() {
        initialScale = transform.localScale;
    }
    void Update()
    {
        // get value from input
        // check if object is hovered or selected
        // check if value read is not null (vector2 not directly y)
        // apply scaling (get new scale and lerp)
        // check range of scale
        if (GetComponent<XRGrabInteractable>().isHovered) { 
            if (inputReference.action.ReadValue<Vector2>() != null) {
                Debug.Log("inside");
                Vector3 v = new Vector3(inputReference.action.ReadValue<Vector2>().y, inputReference.action.ReadValue<Vector2>().y, inputReference.action.ReadValue<Vector2>().y);
                Debug.Log(transform.localScale); // .10
                if (transform.localScale.magnitude < 4*initialScale.magnitude && transform.localScale.magnitude > (0.25*initialScale.magnitude)) {
                    transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale + v, Time.deltaTime * 5);
                    Debug.Log(transform.localScale); // .21
                } 

            }
        }
    }
}
