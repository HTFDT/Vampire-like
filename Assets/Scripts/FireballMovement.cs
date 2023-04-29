using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10f;
    public Animator animator;
    private static readonly int Hit = Animator.StringToHash("Hit");

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        rb.velocity = Vector2.zero;
        animator.SetBool(Hit, true);
    }

    void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
