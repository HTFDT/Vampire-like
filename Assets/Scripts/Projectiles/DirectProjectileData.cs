using UnityEngine;


[CreateAssetMenu(menuName = "Projectiles/New DirectProjectileData", order = 0)]
public class DirectProjectileData : ProjectileData
{
    [SerializeField] private float speed;
    
    public override void StartActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        base.StartActions(rb, next);
        rb.freezeRotation = true;
    }

    public override void UpdateActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        base.UpdateActions(rb, next);
        var transform = rb.transform;
        transform.position += transform.right * speed * Time.fixedDeltaTime;
        next?.Action.Invoke(rb, next.Next);
    }
}