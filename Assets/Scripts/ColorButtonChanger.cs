using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButtonChanger : MonoBehaviour
{
    public MeshRenderer _outerRenderer;

    public ControllerPointer _controller;

    Color highlightColor = Color.white;

    Color startingColor;

    Color buttonColor;




    private void Awake() {
        startingColor = _outerRenderer.material.color;
        buttonColor = GetComponent<MeshRenderer>().material.color;
    }
    public void Highlight()
    {
        _outerRenderer.material.color = highlightColor;
        _controller.SetColor(buttonColor);
    }

    public void Dehighlight()
    {
        _outerRenderer.material.color = startingColor;
    }
    public Color GetColor() {
        return startingColor;
    }
}

