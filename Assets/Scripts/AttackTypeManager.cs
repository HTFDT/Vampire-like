using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypeManager
{
    public List<ModifierCount> Modifiers;
    public List<ProjectileData> Projectiles;
    public float AttackDelay;
    public readonly Func<Transform, GameObject, GameObject, List<ProjectileData>, float, List<ModifierCount>, IEnumerator> LaunchCoroutine;

    public AttackTypeManager(AttackTypeData data)
    {
        Modifiers = data.DefaultModifiers;
        Projectiles = data.DefaultProjectiles;
        AttackDelay = data.DefaultAttackDelay;
        LaunchCoroutine = data.LaunchAttackCycle;
    }
}