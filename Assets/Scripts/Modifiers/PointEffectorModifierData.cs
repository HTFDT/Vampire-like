using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New PointEffectorModifierData")]
public class PointEffectorModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.Initialization;
    public float forceMagnitude;
    public float drag;
    public bool colliderIsTrigger;
    
    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Projectile>().StartActions.AddLast(ApplyEffector);
    }

    private void ApplyEffector(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        var collider = rb.gameObject.GetComponent<Collider2D>();
        collider.usedByEffector = true;
        collider.isTrigger = colliderIsTrigger;
        var effector = rb.gameObject.AddComponent<PointEffector2D>();
        effector.useColliderMask = false;
        effector.forceMagnitude = forceMagnitude;
        effector.drag = drag;
    }
}