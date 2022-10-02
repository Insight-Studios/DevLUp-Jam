using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Doors doors;
    [SerializeField] Sprite[] sprites;

    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
    }

    public void Close()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    // Update is called once per frame
    void OnMouseUpAsButton()
    {
        doors.DoorClicked(this);
    }
}
