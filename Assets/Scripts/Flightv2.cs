using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Flightv2 : MonoBehaviour
{
    public Transform leftPad;
    public Transform rightPad;

    public InputActionReference _leftFlightButton;
    public InputActionReference _rightFlightButton;
    public float flapForce = 0.5f;
    private Rigidbody _rigidBody;
    private bool leftArmActivated;
    private bool rightArmActivated;
    private Vector3 leftHandDownVector;
    private Vector3 rightHandDownVector;
    // Start is called before the first frame update
    void Start()
    {
        _leftFlightButton.action.started += LeftFlightActivate;
        _leftFlightButton.action.canceled += LeftFlightDeactivate;
        _rightFlightButton.action.started += RightFlightActivate;
        _rightFlightButton.action.canceled += RightFlightDeactivate;
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        leftArmActivated = false;

        // Vectors
        rightHandDownVector = new Vector3(0, 0, 0);
        leftHandDownVector = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rightHandDownVector = rightPad.right.normalized;
        leftHandDownVector = leftPad.right.normalized * -1.0f;
        if (leftArmActivated)
        {
            _rigidBody.AddForce(leftHandDownVector * flapForce, ForceMode.Impulse);
        }
        if (rightArmActivated)
        {
            _rigidBody.AddForce(rightHandDownVector * flapForce, ForceMode.Impulse);
        }

    }
    private void LeftFlightActivate(InputAction.CallbackContext context)
    {
        leftArmActivated = true;
    }
    private void LeftFlightDeactivate(InputAction.CallbackContext context)
    {
        leftArmActivated = false;
    }

    private void RightFlightActivate(InputAction.CallbackContext context)
    {
        rightArmActivated = true;
    }
    private void RightFlightDeactivate(InputAction.CallbackContext context)
    {
        rightArmActivated = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(leftPad.position, leftPad.position + leftHandDownVector);
    }
}
