using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FlightUtils))]
[RequireComponent(typeof(Rigidbody))]
public class Flight : MonoBehaviour
{
    public float flapForce = 1f;
    public float upwardFlapReduceForce = 0.2f;
    public float gravityModifier = 1f;
    public float dragModifier = 2f;
    public Transform forwardCameraReference;
    public Transform leftReference;
    public Transform rightReference;
    public InputActionReference _leftVelocityReference;
    public InputActionReference _leftFlightButton;
    public InputActionReference _rightVelocityReference;
    public InputActionReference _rightFlightButton;
    private bool leftArmActivated = false;
    private bool rightArmActivated = false;
    private Rigidbody _rigidBody;
    private FlightUtils utilsFlight;

    void Awake()
    {
        _leftFlightButton.action.started += LeftFlightActivate;
        _leftFlightButton.action.canceled += LeftFlightDeactivate;
        _rightFlightButton.action.started += RightFlightActivate;
        _rightFlightButton.action.canceled += RightFlightDeactivate;
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.useGravity = false;
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        utilsFlight = GetComponent<FlightUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 leftDownVector = leftReference.up * -1.0f;
        Debug.Log(leftDownVector);
        ///////// Gravity 
        float percentHandsApartHorizontal = utilsFlight.DistanceHandsHorizontal() / utilsFlight.maxWingSpan;
        if (percentHandsApartHorizontal < 0.3f) // If below threshold, act as if hands together
        {
            percentHandsApartHorizontal = 0.0f;
        }
        Vector3 gravity = Physics.gravity;
        if (_rigidBody.velocity.z < 0.0f) // If player is moving down, slow down gravity when arms open
        {
            gravity *= gravityModifier * (1 - percentHandsApartHorizontal);
        }
        _rigidBody.AddForce(gravity, ForceMode.Acceleration);

        ///////// Flap force 
        Vector3 leftVelocity = leftArmActivated ? _leftVelocityReference.action.ReadValue<Vector3>() : new Vector3(0, 0, 0);
        Vector3 rightVelocity = rightArmActivated ? _rightVelocityReference.action.ReadValue<Vector3>() : new Vector3(0, 0, 0);
        Vector3 totalVelocity = leftVelocity + rightVelocity;
        totalVelocity *= flapForce;
        totalVelocity *= -1;

        // Reduce downward flap force
        if (totalVelocity.z < 0)
        {
            totalVelocity *= upwardFlapReduceForce;
        }

        Vector3 worldVelocity = forwardCameraReference.TransformDirection(totalVelocity);

        _rigidBody.AddForce(worldVelocity * flapForce, ForceMode.Impulse);

        Debug.Log(utilsFlight.AngleDownLeft());
        Debug.Log(utilsFlight.AngleDownRight());
    }

    private void RightFlightActivate(InputAction.CallbackContext context)
    {
        rightArmActivated = true;
    }
    private void RightFlightDeactivate(InputAction.CallbackContext context)
    {
        rightArmActivated = false;
    }
    private void LeftFlightActivate(InputAction.CallbackContext context)
    {
        leftArmActivated = true;
    }
    private void LeftFlightDeactivate(InputAction.CallbackContext context)
    {
        leftArmActivated = false;
    }


}
