using UnityEngine;


[CreateAssetMenu(menuName = "Projectiles/New DirectProjectileData", order = 0)]
public class DirectProjectileData : ProjectileData
{
    [SerializeField] private float speed;
    
    public override void AwakeActions(Rigidbody2D rb)
    {
        base.AwakeActions(rb);
        rb.freezeRotation = true;
    }

    public override void UpdateActions(Rigidbody2D rb)
    {
        base.UpdateActions(rb);
        var transform = rb.transform;
        transform.position += transform.right * speed * Time.fixedDeltaTime;
    }
}