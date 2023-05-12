using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ProjectileData : ScriptableObject
{
    [SerializeField] private float baseDamage;
    [SerializeField] private List<ModifierCount> baseModifiers;
    public float BaseDamage => baseDamage;
    public List<ModifierCount> BaseModifiers => baseModifiers;
    public AttackTypesEnum attackType;

    public virtual void StartActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        next?.Action(rb, next.Next);
    }

    public virtual void UpdateActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        next?.Action(rb, next.Next);
    }

    public virtual void OnCollisionActions(Collision2D col, ChainNode<Collision2D> next)
    {
        next?.Action(col, next.Next);
    }

    public virtual void OnDestroyActions(ChainNode next)
    {
        next?.Action(next.Next);   
    }
    
}