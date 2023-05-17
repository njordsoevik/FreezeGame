using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPointer : MonoBehaviour
{
    MeshRenderer _controllerRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _controllerRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    public Color GetColor() {
        return _controllerRenderer.material.color;
    }

    public void SetColor(Color c) {
        _controllerRenderer.material.color = c;
    }
}
