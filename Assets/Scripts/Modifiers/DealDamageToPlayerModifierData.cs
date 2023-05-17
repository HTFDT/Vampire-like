using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New DealDamageToPlayerModifierData")]
public class DealDamageToPlayerModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        var script = projectile.GetComponent<Projectile>();
        script.OnCollisionActions.AddLast(DealDamage);

        void DealDamage(Collision2D col, ChainNode<Collision2D> next)
        {
            next?.Action(col, next.Next);
            if (!col.gameObject.CompareTag("Player")) return;
            col.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(script.data.BaseDamage);
        }
    }
}