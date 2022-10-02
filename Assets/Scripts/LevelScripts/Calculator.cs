using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{

    [SerializeField] TextMeshPro display;
    [SerializeField] float numberOneSize;

    public List<NumberOne> ones = new List<NumberOne>();

    private string displayText {
        get {
            return display.text.Substring(16, display.text.Length - 9 - 16);
        } set
        {
            while (ones.Count > 0)
            {
                Destroy(ones[ones.Count - 1].gameObject);
                ones.RemoveAt(ones.Count - 1);
            }

            int i = value.IndexOf('1');
            while (i != -1 && i < value.Length)
            {
                ones.Add(Instantiate<NumberOne>(
                    GameManager.NumberOnePrefab, 
                    transform.position + Vector3.up * 4 + Vector3.right * (4f - value.Length + i),
                    Quaternion.identity,
                    transform
                ));
                ones[ones.Count - 1].transform.localScale *= numberOneSize;
                i = value.IndexOf('1', i + 1);
            }

            display.text = "<mspace=1.125em>" + value.Replace('1', '_') + "</mspace>";
        }
    }

    int memory = 0;
    string operation = "";
    bool displayed = true;

    // Start is called before the first frame update
    void Start()
    {
        displayText = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Compute()
    {
        displayed = true;
        int input;
        int.TryParse(displayText, out input);

        switch (operation)
        {
            case "+":
                displayText = (memory + input).ToString();
                break;
            case "-":
                displayText = (memory - input).ToString();
                break;
            case "*":
                displayText = (memory * input).ToString();
                break;
            case "/":
                displayText = (memory / input).ToString();
                break;
            case "":
                break;
        }
        if (displayText.Length > 4)
        {
            displayText = "ERR";
        }
        operation = "";
    }

    public void ButtonClicked(string button)
    {
        switch(button)
        {
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
                if (displayed)
                {
                    int.TryParse(displayText, out memory);
                    displayText = "";
                    displayed = false;
                }
                if (displayText.Length < 4)
                {
                    displayText += button;
                }
                break;
            case "+":
            case "-":
            case "*":
            case "/":
                Compute();
                operation = button;
                break;
            case "=":
                Compute();
                break;
        }
    }
}
