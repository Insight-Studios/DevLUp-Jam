using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownRandom : MonoBehaviour
{
    [SerializeField]
    Vector2 screenDim;
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
        transform.position = new Vector3(Random.Range(-screenDim.x, screenDim.x), Random.Range(-screenDim.y, screenDim.y), 0);
        transform.localScale = Vector3.one * Random.Range(0.2f, 0.8f);

        count--;
        tm.text = count + "";

        if (count == 1)
        {
            one.SetActive(true);
            Destroy(gameObject);
        }
    }
}
