using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New FreezePositionModifierData")]
public class FreezePositionModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.Initialization;
    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}