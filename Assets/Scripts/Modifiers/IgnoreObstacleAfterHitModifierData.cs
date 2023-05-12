using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New IgnoreObstacleAfterHitModifierData")]
public class IgnoreObstacleAfterHitModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Projectile>().OnCollisionActions.AddLast(IgnoreIncomingCollision);
    }

    private void IgnoreIncomingCollision(Collision2D col, ChainNode<Collision2D> next)
    {
        if (col.gameObject.CompareTag("Obstacle"))
            Physics2D.IgnoreCollision(col.collider, col.otherCollider);
        
        next?.Action(col, next.Next);
    }
}