using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int pos;
    [HideInInspector]
    public Draw draw;

    void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            draw.Click(pos);
        }
    }
    void OnMouseDown()
    {
        draw.Click(pos);
    }
}
