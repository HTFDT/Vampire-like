using UnityEngine;


[CreateAssetMenu(menuName = "Projectiles/New ExplosionProjectileData")]
public class ExplosionProjectileData : ProjectileData
{
    public override void StartActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        base.StartActions(rb, next);
        rb.transform.rotation = Quaternion.identity;
    }
}