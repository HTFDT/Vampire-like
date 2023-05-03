using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ProjectileData : ScriptableObject
{
    [SerializeField] private float baseDamage;
    [SerializeField] private List<ModifierData> baseModifiers;
    public float BaseDamage => baseDamage;
    public List<ModifierData> BaseModifiers => baseModifiers;

    public virtual void AwakeActions(Rigidbody2D rb)
    {
    }

    public virtual void StartActions(Rigidbody2D rb)
    {
    }

    public virtual void UpdateActions(Rigidbody2D rb)
    {
    }

    public virtual void OnCollisionActions(Collision2D col)
    {
    }

    public virtual void OnDestroyActions(Rigidbody2D rb)
    {
        
    }
    
}