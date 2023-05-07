using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action<Rigidbody2D> StartActions;
    public Action<Rigidbody2D> UpdateActions;
    public Action<Collision2D> OnCollisionActions;
    public Action<Rigidbody2D> OnDestroyActions;
    public Rigidbody2D rb;

    private void Start()
    {
        StartActions?.Invoke(rb);
    }
    
    private void FixedUpdate()
    {
        UpdateActions?.Invoke(rb);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnCollisionActions?.Invoke(col);
    }

    private void OnDestroy()
    {
        OnDestroyActions?.Invoke(rb);
    }
}
