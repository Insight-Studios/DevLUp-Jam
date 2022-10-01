using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int index;

    public void setPos(int x, int y)
    {
        transform.position = new Vector3(x * transform.localScale.x, y * transform.localScale.y, 0);
    }
    public void movePos(int x, int y)
    {
        transform.position += new Vector3(x * transform.localScale.x, y * transform.localScale.y, 0);
    }

    void OnMouseUp()
    {
        GetComponentInParent<Blocks>().Click(Mathf.RoundToInt(transform.position.x / transform.localScale.x) + 1, Mathf.RoundToInt(transform.position.y / transform.localScale.y) + 1);
    }
}
