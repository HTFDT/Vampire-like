using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New CircleColliderModifier")]
public class CircleColliderModifier : ModifierData
{
    public override void ApplyTo(GameObject proj)
    {
        base.ApplyTo(proj);
        var container = proj.AddComponent<ColliderInfoContainer>();
        container.circleCollider = proj.AddComponent<CircleCollider2D>();
        container.spriteRenderer = proj.GetComponent<SpriteRenderer>();
        proj.GetComponent<Projectile>().UpdateActions += KeepColliderForm;
    }

    private void KeepColliderForm(Rigidbody2D rb)
    {
        var container = rb.gameObject.GetComponent<ColliderInfoContainer>();
        container.circleCollider.radius = container.spriteRenderer.size.x / 2;
    }
}