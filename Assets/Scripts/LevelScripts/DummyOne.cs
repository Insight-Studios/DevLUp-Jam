using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyOne : MonoBehaviour
{
    [SerializeField]
    float angularSpeed = 1;
    [SerializeField]
    float startVelocity = 5;

    void OnMouseUp()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        rb.angularVelocity = 360 * angularSpeed;
        rb.velocity = new Vector2(startVelocity / 2, startVelocity);
        Destroy(gameObject, 5);
    }
}
