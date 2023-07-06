using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightWingspan : MonoBehaviour
{
    public InputActionReference _leftHandPosition;
    public InputActionReference _rightHandPosition;
    public float maxWingSpan = 1.7f; // TODO: use GetHandsDistanceHorizontal in a config scene

}
