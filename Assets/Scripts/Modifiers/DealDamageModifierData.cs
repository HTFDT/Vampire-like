using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/new DealDamageModifierData")]
public class DealDamageModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Projectile>().OnCollisionActions.AddLast(DealDamage);
    }

    private void DealDamage(Collision2D col, ChainNode<Collision2D> next)
    {
        var other = col.gameObject;
        if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().TakeDamage(col.otherRigidbody.gameObject.GetComponent<Projectile>().data.BaseDamage);
        next?.Action.Invoke(col, next.Next);
    }
}