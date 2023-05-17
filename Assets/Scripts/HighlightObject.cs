using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    Color highlightColor = Color.white;

    MeshRenderer _renderer;
    public Color originalColor;
    bool isHighlighted = false;

    void Awake()
    {
        // get a reference to the MeshRenderer
        _renderer = GetComponent<MeshRenderer>();
        // access and store the originalColor
        originalColor = _renderer.material.color;
    }

    void Highlight()
    {
        // set isHighlighted true
        isHighlighted = true;
        // change the material color to highlightColor

        _renderer.material.color = highlightColor;
    }

    void Dehighlight()
    {
        // set isHighlighted false
        isHighlighted = false;
        // change the material color to originalColor
        _renderer.material.color = originalColor;
    }

    public void ToggleHighlight()
    {
        
        // if not already highlighted, highlight the object
        if (!isHighlighted) {
            Highlight();
        } else { // else dehighlight it
            Dehighlight();
        }
    }

    public Color GetOGColor() {
        return originalColor;
    }

    
    public void SetOGColor(Color color) {
        originalColor = color;
    }
}
