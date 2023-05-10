using UnityEngine;


[CreateAssetMenu(menuName = "Projectiles/New CircleAttackProjectileData")]
public class CircleAttackProjectileData : ProjectileData
{
    public float maxRadius;
    public float radiusIncrement;
    
    public override void UpdateActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        base.UpdateActions(rb, next);
        var spriteRenderer = rb.gameObject.GetComponent<SpriteRenderer>();
        var size = spriteRenderer.size;
        var ratio = size.x / size.y;
        spriteRenderer.size = new Vector2(size.x + radiusIncrement, size.y + radiusIncrement / ratio);
        if (spriteRenderer.size.x >= maxRadius)
            Destroy(rb.gameObject);
    }
}