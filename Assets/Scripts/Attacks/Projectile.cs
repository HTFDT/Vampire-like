using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    public Animator animator;
    public float moveSpeed;
    public float baseDamage;
    public Rigidbody2D rb;
    public Action<Collision2D> OnCollisionEnter;

    public void Init(ProjectileData pdata)
    {
        data = pdata;
        animator.runtimeAnimatorController = data.MainAnimatorController;
        moveSpeed = data.MoveSpeed;
        baseDamage = data.BaseDamage;
        OnCollisionEnter += data.OnCollision;
    }
    
    private void Start()
    {
        data.StartActions(rb);
    }
    
    private void FixedUpdate()
    {
        data.UpdateActions(rb);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnCollisionEnter?.Invoke(col);
    }
}
