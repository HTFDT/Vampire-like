using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New CircleColliderModifier")]
public class CircleColliderModifier : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnUpdateAction;

    public override void ApplyTo(GameObject proj, int modifierCount)
    {
        var container = proj.AddComponent<ColliderInfoContainer>();
        container.circleCollider = proj.AddComponent<CircleCollider2D>();
        container.spriteRenderer = proj.GetComponent<SpriteRenderer>();
        proj.GetComponent<Projectile>().UpdateActions.AddLast(KeepColliderForm);
    }

    private void KeepColliderForm(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        var container = rb.gameObject.GetComponent<ColliderInfoContainer>();
        container.circleCollider.radius = container.spriteRenderer.size.x / 2;
        next?.Action.Invoke(rb, next.Next);
    }
}