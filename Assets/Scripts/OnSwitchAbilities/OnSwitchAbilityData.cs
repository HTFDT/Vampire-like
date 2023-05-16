using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnSwitchAbilityData : ScriptableObject
{
    public abstract void Apply(GameObject player, IEnumerable<ModifierCount> modifiers);
}
