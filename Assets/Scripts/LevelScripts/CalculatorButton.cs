using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorButton : MonoBehaviour
{

    [SerializeField] string button;
    [SerializeField] Calculator calculator;

    private void OnMouseUpAsButton()
    {
        calculator.ButtonClicked(button);
    }
}
