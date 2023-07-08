using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightUtils : MonoBehaviour
{
    public InputActionReference _headPosition;
    public InputActionReference _leftHandPosition;
    public InputActionReference _rightHandPosition;
    public InputActionReference _leftVelocityReference;
    public InputActionReference _rightVelocityReference;
    public float maxWingSpan = 1.7f; // TODO: use GetHandsDistanceHorizontal in a config scene

    public Vector3 LeftHandVelocity()
    {
        return _leftVelocityReference.action.ReadValue<Vector3>();
    }

    public Vector3 RightHandVelocity()
    {
        return _rightVelocityReference.action.ReadValue<Vector3>();
    }

    public float DistanceHandsHorizontal()
    {
        Vector3 lPosition = _leftHandPosition.action.ReadValue<Vector3>();
        Vector3 rPosition = _rightHandPosition.action.ReadValue<Vector3>();

        lPosition.z = 0.0f;
        rPosition.z = 0.0f;

        return Vector3.Distance(lPosition, rPosition);
    }

    public float DistanceHandsVertical()
    {
        Vector3 lPosition = _leftHandPosition.action.ReadValue<Vector3>();
        Vector3 rPosition = _rightHandPosition.action.ReadValue<Vector3>();

        lPosition.x = 0.0f;
        rPosition.x = 0.0f;

        lPosition.y = 0.0f;
        rPosition.y = 0.0f;

        return Vector3.Distance(lPosition, rPosition);
    }

    public float DistanceTotalHands()
    {
        Vector3 lPosition = _leftHandPosition.action.ReadValue<Vector3>();
        Vector3 rPosition = _rightHandPosition.action.ReadValue<Vector3>();

        return Vector3.Distance(lPosition, rPosition);
    }

    public float HandHeightLeft()
    {
        Debug.Log("-----------START---------");
        Debug.Log(_leftHandPosition.action.ReadValue<Vector3>());
        Debug.Log(_leftHandPosition.action.ReadValue<Vector3>().y);
        return _leftHandPosition.action.ReadValue<Vector3>().y;
    }

    public float HandHeightRight()
    {
        return _leftHandPosition.action.ReadValue<Vector3>().y;
    }

    public float AngleDownRight()
    {

        Vector3 headToRight = Vector3.Normalize(HeadToRightVector());
        return Vector3.Angle(headToRight, Vector3.down);
    }

    public float AngleDownLeft()
    {

        Vector3 headToLeft = Vector3.Normalize(HeadToLeftVector());
        return Vector3.Angle(headToLeft, Vector3.down);
    }

    private Vector3 HeadToRightVector()
    {
        Vector3 rPosition = _rightHandPosition.action.ReadValue<Vector3>();
        Vector3 headPosition = _headPosition.action.ReadValue<Vector3>();

        return rPosition - headPosition;
    }

    private Vector3 HeadToLeftVector()
    {
        Vector3 lPosition = _leftHandPosition.action.ReadValue<Vector3>();
        Vector3 headPosition = _headPosition.action.ReadValue<Vector3>();
        return lPosition - headPosition;
    }
    public float HeadToLeftDistance()
    {
        Vector3 lPosition = _leftHandPosition.action.ReadValue<Vector3>();
        Vector3 headPosition = _headPosition.action.ReadValue<Vector3>();
        lPosition.y = 0.0f;
        headPosition.y = 0.0f;
        return Vector3.Distance(lPosition, headPosition);
    }

    public float HeadToRightDistance()
    {
        Vector3 rPosition = _rightHandPosition.action.ReadValue<Vector3>();
        Vector3 headPosition = _headPosition.action.ReadValue<Vector3>();
        rPosition.y = 0.0f;
        headPosition.y = 0.0f;
        return Vector3.Distance(rPosition, headPosition);
    }

}
