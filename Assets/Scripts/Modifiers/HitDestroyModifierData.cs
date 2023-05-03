using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New HitDestroyModifierData")]
public class HitDestroyModifierData : ModifierData
{
    public override void ApplyTo(GameObject proj)
    {
        base.ApplyTo(proj);
        proj.GetComponent<Projectile>().OnCollisionActions += DestroyOnCollision;
    }

    private void DestroyOnCollision(Collision2D col)
    {
        Destroy(col.otherRigidbody.gameObject);
    }
}