using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New BoxColliderModifierData")]
public class BoxColliderModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnUpdateAction;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.AddComponent<BoxCollider2D>();
        projectile.GetComponent<Projectile>().UpdateActions.AddLast(KeepColliderForm);
    }

    private void KeepColliderForm(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        var collider = rb.gameObject.GetComponent<BoxCollider2D>();
        var spriteRenderer = rb.gameObject.GetComponent<SpriteRenderer>();
        collider.size = spriteRenderer.size;
        next?.Action.Invoke(rb, next.Next);
    }
}