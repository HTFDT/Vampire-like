using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectiles/New DirectProjectile")]
public class DirectProjectile : ProjectileData
{
    public override void StartActions(Rigidbody2D rb)
    {
        rb.velocity = rb.transform.right * MoveSpeed;
    }
}
