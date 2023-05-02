using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    public Rigidbody2D rb;
    public Animator animator;
    public Action<Collision2D> OnCollisionActions;
    public Action<Rigidbody2D> StartActions;
    public Action<Rigidbody2D> UpdateActions;

    public void Init(ProjectileData pdata)
    {
        data = pdata;
        animator.runtimeAnimatorController = data.MainAnimatorController;
        OnCollisionActions += data.OnCollisionActions;
        StartActions += data.StartActions;
        UpdateActions += data.UpdateActions;
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
}
