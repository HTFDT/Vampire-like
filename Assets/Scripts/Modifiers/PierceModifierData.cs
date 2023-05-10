using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/New PierceModifierData")]
public class PierceModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;

    public override void ApplyTo(GameObject proj, int modifierCount)
    {
        var controller = proj.gameObject.AddComponent<PierceController>();
        controller.piercesAvailable = modifierCount;
        proj.GetComponent<Projectile>().OnCollisionActions.AddLast(HandlePiercingOnCollision);
        
    }

    private void HandlePiercingOnCollision(Collision2D col, ChainNode<Collision2D> next)
    {
        var controller = col.otherCollider.GetComponent<PierceController>();
        if (col.gameObject.CompareTag("Enemy") && --controller.piercesAvailable >= 0)
        {
            Physics2D.IgnoreCollision(col.collider, col.otherCollider);
            if (!next.IsLast)
                next.Action(col, next.Next);
            else
                return;
        }

        next?.Action(col, next.Next);
    }
}