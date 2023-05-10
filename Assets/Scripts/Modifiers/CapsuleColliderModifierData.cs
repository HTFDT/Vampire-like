using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New CapsuleColliderModifierData")]
public class CapsuleColliderModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnUpdateAction;
    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.AddComponent<CapsuleCollider2D>();
        projectile.GetComponent<Projectile>().UpdateActions.AddLast(KeepColliderForm);
    }
    
    private void KeepColliderForm(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        var collider = rb.gameObject.GetComponent<CapsuleCollider2D>();
        var spriteRenderer = rb.gameObject.GetComponent<SpriteRenderer>();
        var size = spriteRenderer.size;
        collider.direction = size.x > size.y ? CapsuleDirection2D.Horizontal : CapsuleDirection2D.Vertical;
        collider.size = size;
        next?.Action.Invoke(rb, next.Next);
    }
}