using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Renderer component;

    [SerializeField] Doors doors;

    private void Start()
    {
        component = GetComponent<Renderer>();
    }

    public void Open()
    {
        component.enabled = false;
    }

    public void Close()
    {
        component.enabled = true;
    }

    // Update is called once per frame
    void OnMouseUpAsButton()
    {
        doors.DoorClicked(this);
    }
}
