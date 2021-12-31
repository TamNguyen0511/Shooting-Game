using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;

    float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
