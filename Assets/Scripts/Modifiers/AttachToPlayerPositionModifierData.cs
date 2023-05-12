using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/New AttachToPlayerPositionModifierData")]
public class AttachToPlayerPositionModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnUpdateAction;
    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        var firePoint = GameObject.FindWithTag("Player").transform.Find("FirePoint");
        projectile.GetComponent<Projectile>().UpdateActions.AddLast((rb, next) =>
        {
            projectile.transform.position = firePoint.position;
            next?.Action(rb, next.Next);
        });
    }
}