using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Projectiles/New HitDestroyProjectile")]
public class HitDestroyProjectile : DirectProjectile
{
    public override void OnCollision(Collision2D col)
    {
        Debug.Log("On Collision");
        Destroy(col.otherCollider.gameObject);
        var transform = col.otherRigidbody.transform;
        var impactEffect = Instantiate(ImpactEffect, transform.position, transform.rotation);
        impactEffect.GetComponent<ImpactEffect>().Init(ImpactEffectData);
    }
}

