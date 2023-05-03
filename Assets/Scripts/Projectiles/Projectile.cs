using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    public Rigidbody2D rb;
    public Action<Collision2D> OnCollisionActions;
    public Action<Rigidbody2D> StartActions;
    public Action<Rigidbody2D> UpdateActions;
    public Action<Rigidbody2D> AwakeActions;
    public Action<Rigidbody2D> OnDestroyActions;

    public void Init(ProjectileData pdata)
    {
        data = pdata;
        OnCollisionActions += data.OnCollisionActions;
        StartActions += data.StartActions;
        UpdateActions += data.UpdateActions;
        AwakeActions += data.AwakeActions;
        OnDestroyActions += data.OnDestroyActions;
        
        foreach (var mod in data.BaseModifiers)
            mod.ApplyTo(gameObject);
    }

    private void Awake()
    {
        AwakeActions?.Invoke(rb);
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
}
