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
        if (spriteRenderer.sprite == null) return;
        var sprite = spriteRenderer.sprite;
        collider.direction = sprite.rect.size.x > sprite.rect.size.y ? CapsuleDirection2D.Horizontal : CapsuleDirection2D.Vertical;
        collider.size = sprite.rect.size / sprite.pixelsPerUnit;
        next?.Action.Invoke(rb, next.Next);
    }
}