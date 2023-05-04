using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New HitDestroyModifierData")]
public class HitDestroyModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.Destroying;

    public override void ApplyTo(GameObject proj, int modifierCount)
    {
        base.ApplyTo(proj, modifierCount);
        proj.GetComponent<Projectile>().OnCollisionActions.AddLast(DestroyOnCollision);
    }

    private void DestroyOnCollision(Collision2D col, ChainNode<Collision2D> next)
    {
        Destroy(col.otherRigidbody.gameObject);
        next?.Action(col, next.Next);
    }
}