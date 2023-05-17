using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuRotation : MonoBehaviour
{
    public InputActionReference inputReference = null;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (inputReference.action.ReadValue<Vector2>().x != 0) {
            transform.Rotate(0f, inputReference.action.ReadValue<Vector2>().x, 0f);
        }

    }
}