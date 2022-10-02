using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField]
    float speed;

    Animator an;

    void Start()
    {
        an = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;

        int dirIndex = -1;
        if (dir.x == 1)
        {
            dirIndex = 0;
        }
        else if (dir.x == -1)
        {
            dirIndex = 2;
        }
        else if (dir.y == 1)
        {
            dirIndex = 1;
        }
        else if (dir.y == -1)
        {
            dirIndex = 3;
        }
        an.SetInteger("Direction", dirIndex);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NumberOne>() != null)
        {
            GameManager.Instance.NextLevel();
        }
    }
}
