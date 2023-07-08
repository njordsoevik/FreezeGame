using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FlightUtils))]
[RequireComponent(typeof(Rigidbody))]
public class Flightv2 : MonoBehaviour
{
    public Transform leftPad;
    public Transform rightPad;

    public InputActionReference _leftFlightButton;
    public InputActionReference _rightFlightButton;
    public float flapForce = 0.4f;
    public float liftFactor = 0.7f; // between 0,1, how much negating force your wings have against body velocity
    public float rotationSpeed = 1f;
    private Rigidbody _rigidBody;
    private bool leftArmActivated;
    private bool rightArmActivated;
    private Vector3 leftHandDownVector;
    private Vector3 rightHandDownVector;
    private FlightUtils utilsFlight;
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
        rightHandDownVector = rightPad.right.normalized;
        leftHandDownVector = leftPad.right.normalized * -1.0f;
        utilsFlight = GetComponent<FlightUtils>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ///////// Palm direction
        rightHandDownVector = rightPad.right.normalized;
        leftHandDownVector = leftPad.right.normalized * -1.0f;

        ///////// Distance Hands Apart 
        float percentHandsApartLeft = utilsFlight.HeadToLeftDistance() / (utilsFlight.maxWingSpan / 2.0f);

        float percentHandsApartRight = utilsFlight.HeadToRightDistance() / (utilsFlight.maxWingSpan / 2.0f);
        percentHandsApartLeft = Mathf.Min(percentHandsApartLeft, 1.0f);
        percentHandsApartRight = Mathf.Min(percentHandsApartRight, 1.0f);

        if (percentHandsApartLeft < 0.4f) // If below threshold, act as if hands together
        {
            percentHandsApartLeft = 0.0f;
        }
        if (percentHandsApartRight < 0.4f) // If below threshold, act as if hands together
        {
            percentHandsApartRight = 0.0f;
        }

        ///////// Flap force 
        Vector3 leftVelocity = utilsFlight.LeftHandVelocity() * percentHandsApartLeft;
        Vector3 rightVelocity = utilsFlight.RightHandVelocity() * percentHandsApartRight;
        // Less powerful upward motion
        if (leftVelocity.y > 0)
        {
            leftVelocity *= 0.2f;
        }
        if (rightVelocity.y > 0)
        {
            rightVelocity *= 0.2f;
        }
        // Accelerate body with flap force in opposite direction of palm
        _rigidBody.AddForce((leftHandDownVector * -1.0f) * Vector3.Dot(leftVelocity, leftHandDownVector) * flapForce, ForceMode.Impulse);
        _rigidBody.AddForce((rightHandDownVector * -1.0f) * Vector3.Dot(rightVelocity, rightHandDownVector) * flapForce, ForceMode.Impulse);

        //////// Lift
        Vector3 currentVelocity = _rigidBody.velocity;
        // Equation description
        // (leftHandDownVector * -1.0f) is the palm direction to apply our lift force
        // Vector3.Dot(currentVelocity, leftHandDownVector) gets our current velocity in the palms direction, which we want to partially negate
        // 0.5f because one hand gives half the force
        // liftFactor so we don't 100% negate our velocity, only partially
        // percentHandsApart to add control to how much lift we want
        _rigidBody.AddForce((leftHandDownVector * -1.0f) * Vector3.Dot(currentVelocity, leftHandDownVector) * liftFactor * 0.5f * percentHandsApartLeft, ForceMode.Acceleration);
        _rigidBody.AddForce((rightHandDownVector * -1.0f) * Vector3.Dot(currentVelocity, rightHandDownVector) * liftFactor * 0.5f * percentHandsApartRight, ForceMode.Acceleration);


        // This negates gravity fully
        // _rigidBody.AddForce(-1f * Physics.gravity, ForceMode.Acceleration);

        //////// Rotation 
        Debug.Log("utilsFlight.HandHeightLeft()");
        Debug.Log(utilsFlight.HandHeightLeft());
        float heightDifference = utilsFlight.HandHeightRight() - utilsFlight.HandHeightLeft();
        Debug.Log("heightDifference");
        Debug.Log(heightDifference);
        float heightPercent = heightDifference / utilsFlight.maxWingSpan; // TODO Not perfect because height difference cant reach max wing span
        Debug.Log("heightPercent");
        Debug.Log(heightPercent);                                                              // Rotate the camera horizontally around the y-axis

        transform.Rotate(Vector3.up, heightPercent * -1f * rotationSpeed);




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
