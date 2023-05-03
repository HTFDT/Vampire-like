using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/New PierceModifierData")]
public class PierceModifierData : ModifierData
{
    public override void ApplyTo(GameObject proj)
    {
        base.ApplyTo(proj);
        if (!proj.gameObject.TryGetComponent<PierceController>(out var counter))
        {
            counter = proj.gameObject.AddComponent<PierceController>();
            proj.GetComponent<Projectile>().OnCollisionActions += HandlePiercingOnCollision;
        }
        counter.piercesAvailable++;
    }

    private void HandlePiercingOnCollision(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(col.collider, col.otherCollider);
            var controller = col.otherCollider.GetComponent<PierceController>();
            if (--controller.piercesAvailable >= 0) return;
        }
        
        Destroy(col.otherRigidbody.gameObject);
    }
}