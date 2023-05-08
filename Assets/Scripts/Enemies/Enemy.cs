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
    public Action<float, GameObject> HandleDamage;
    public Rigidbody2D rb;
    public float damage;
    public float health;
    public float moveSpeed;

    public void Init(EnemyData data)
    {
        damage = data.Damage;
        health = data.Health;
        moveSpeed = data.MoveSpeed;
        data.ApplyTo(gameObject);
    }

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

    public void TakeDamage(float dmg)
    {
        HandleDamage?.Invoke(dmg, gameObject);
    }
}
