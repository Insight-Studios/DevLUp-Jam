using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField]
    float speed;

    void Update()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector2.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir += Vector2.down;
        }

        GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NumberOne>() != null)
        {
            GameManager.Instance.NextLevel();
        }
    }
}
