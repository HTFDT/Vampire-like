using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action<Rigidbody2D> StartActions;
    public Action<Rigidbody2D> UpdateActions;
    public Action<Collision2D> OnCollisionActions;
    public Action OnDestroyActions;
    public Action<float, GameObject> HandleDamage;
    public HealthBar healthBar;
    public Rigidbody2D rb;
    public float damage;
    public float health;
    public float moveSpeed;
    public float initialMoveSpeed;
    public Color initialColor;
    private float _maxHealth;

    public void Init(EnemyData data)
    {
        damage = data.Damage;
        health = _maxHealth = data.Health;
        moveSpeed = initialMoveSpeed = data.MoveSpeed;
        initialColor = GetComponent<SpriteRenderer>().color;
        data.ApplyTo(gameObject);
        healthBar.SetHealth(health, _maxHealth);
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

    private void OnTriggerStay2D(Collider2D col)
    {
        var obj = col.gameObject;
        if (obj.CompareTag("Player"))
            obj.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    private void OnDestroy()
    {
        OnDestroyActions?.Invoke();
    }

    public void TakeDamage(float dmg)
    {
        HandleDamage?.Invoke(dmg, gameObject);
        healthBar.SetHealth(health, _maxHealth);
    }
}
