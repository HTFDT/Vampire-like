using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ProjectileData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController mainAnimatorController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float baseDamage;
    [SerializeField] private GameObject impactEffectPrefab;
    [SerializeField] private ImpactEffectData impactEffectData;

    public RuntimeAnimatorController MainAnimatorController => mainAnimatorController;
    public float MoveSpeed => moveSpeed;
    public float BaseDamage => baseDamage;
    protected GameObject ImpactEffect => impactEffectPrefab;
    protected ImpactEffectData ImpactEffectData => impactEffectData;
    
    public virtual void StartActions(Rigidbody2D rb)
    {
    }

    public virtual void UpdateActions(Rigidbody2D rb)
    {
        
    }

    public virtual void OnCollision(Collision2D col)
    {
        
    }
}