using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ProjectileData : ScriptableObject
{
    [SerializeField] private float baseDamage;
    [SerializeField] private RuntimeAnimatorController mainAnimatorController;
    public float BaseDamage => baseDamage;
    public RuntimeAnimatorController MainAnimatorController => mainAnimatorController;

    public virtual void StartActions(Rigidbody2D rb)
    {
    }

    public virtual void UpdateActions(Rigidbody2D rb)
    {
        
    }

    public virtual void OnCollisionActions(Collision2D col)
    {
        
    }
}