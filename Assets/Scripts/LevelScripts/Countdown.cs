using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    GameObject one;
    TextMesh tm;
    int count;

    void Start()
    {
        one.SetActive(false);
        tm = GetComponent<TextMesh>();
        count = 9;
        tm.text = count + "";
    }

    void OnMouseUp()
    {
        count--;
        tm.text = count + "";

        if (count == 1)
        {
            one.SetActive(true);
            Destroy(gameObject);
        }
    }
}
