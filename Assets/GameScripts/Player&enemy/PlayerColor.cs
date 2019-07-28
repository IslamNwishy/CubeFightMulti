using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerColor : MonoBehaviour
{

    private SpriteRenderer sr;
    private Color col;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void OnSetColor(Color color)
    {
        col = color;
        sr.color = color;
    }

    public void OnApplyColor()
    {
        PlayerPrefs.SetString("RGBColor", col.r + "," + col.g + "," + col.b);
    }


}
