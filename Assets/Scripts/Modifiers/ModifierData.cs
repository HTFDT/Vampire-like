using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New ModifierData")]
public abstract class ModifierData : ScriptableObject
{
    public abstract ModifierTag Tag { get; }
    [Tooltip("Applying order weight")]
    public int weight;

    public virtual void ApplyTo(GameObject projectile, int modifierCount)
    {
    }
}