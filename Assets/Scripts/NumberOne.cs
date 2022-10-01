using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOne : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        GameManager.Instance.NextLevel();
    }
}
