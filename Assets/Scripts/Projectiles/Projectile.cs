using System;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    public Rigidbody2D rb;
    public DelegateChain<Collision2D> OnCollisionActions;
    public DelegateChain<Rigidbody2D> StartActions;
    public DelegateChain<Rigidbody2D> UpdateActions;
    public DelegateChain<Rigidbody2D> OnDestroyActions;

    public void Init(ProjectileData pdata)
    {
        OnCollisionActions = new DelegateChain<Collision2D>();
        StartActions = new DelegateChain<Rigidbody2D>();
        UpdateActions = new DelegateChain<Rigidbody2D>();
        OnDestroyActions = new DelegateChain<Rigidbody2D>();
        
        data = pdata;
        OnCollisionActions.AddLast(data.OnCollisionActions);
        StartActions.AddLast(data.StartActions);
        UpdateActions.AddLast(data.UpdateActions);
        OnDestroyActions.AddLast(data.OnDestroyActions);
    }

    private void Start()
    {
        StartActions.First?.Action(rb, StartActions.First?.Next);
    }
    
    private void FixedUpdate()
    {
        UpdateActions.First?.Action(rb, UpdateActions.First?.Next);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnCollisionActions.First?.Action(col, OnCollisionActions.First?.Next);
    }

    private void OnDestroy()
    {
        OnDestroyActions.First?.Action(rb, OnDestroyActions.First?.Next);
    }
}
