using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New ModifierData")]
public abstract class ModifierData : ScriptableObject
{
    // public abstract string Tag { get; protected set; }

    public virtual void ApplyTo(GameObject projectile)
    {
    }
}