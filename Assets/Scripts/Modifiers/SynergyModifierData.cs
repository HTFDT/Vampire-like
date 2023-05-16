using System.Linq;
using UnityEngine;


public abstract class SynergyModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;
    public AttackTypesEnum withAttackType;
}