using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorButton : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField] string button;
    [SerializeField] Calculator calculator;

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
    void OnMouseUpAsButton()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        calculator.ButtonClicked(button);
    }
}
