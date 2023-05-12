using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New IgnoreEnemiesAfterHitModifierData")]
public class IgnoreEnemiesAfterHitModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Projectile>().OnCollisionActions.AddLast(IgnoreIncomingCollision);
    }

    private void IgnoreIncomingCollision(Collision2D col, ChainNode<Collision2D> next)
    {
        if (col.gameObject.CompareTag("Enemy"))
            Physics2D.IgnoreCollision(col.collider, col.otherCollider);
        
        next?.Action(col, next.Next);
    }
}