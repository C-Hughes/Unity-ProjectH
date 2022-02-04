using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightMoveTo : MonoBehaviour
{
    private Color startcolor;
    void OnMouseEnter()
    {
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = new Color32(255, 191, 0, 99);
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startcolor;
    }
}




