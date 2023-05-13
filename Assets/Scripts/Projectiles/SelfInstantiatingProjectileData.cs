using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Projectiles/New SelfInstantiatingProjectile")]
public class SelfInstantiatingProjectileData : ProjectileData
{
    public GameObject projectilePrefab;
    public float delayBeforeNextSegmentSpawn;
    public float distanceBetweenSegmentsRelativeToSize;

    public override void StartActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        base.StartActions(rb, next);
        var camProps = Camera.main!.GetComponent<CameraProps>();
        var script = rb.gameObject.GetComponent<Projectile>();
        var projectileContainer = GameObject.FindWithTag("ProjectileContainer");
        if (!camProps.InBounds(rb.position)) return;
        script.StartCoroutine(SpawnNextSegment(rb, script, projectileContainer.transform));
    }

    private IEnumerator SpawnNextSegment(Rigidbody2D rb, Projectile script, Transform parent)
    {
        yield return new WaitForSeconds(delayBeforeNextSegmentSpawn);
        var transform = rb.transform;
        var spriteRenderer = rb.gameObject.GetComponent<SpriteRenderer>();
        var proj = Instantiate(projectilePrefab,
            rb.position + (Vector2)(transform.right * (spriteRenderer.size.x * distanceBetweenSegmentsRelativeToSize)),
            transform.rotation, parent);
        proj.SetActive(false);
        var sc = proj.GetComponent<Projectile>();
        sc.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        sc.Init(script.data, script.AdditionalModifiers);
        proj.SetActive(true);
    }
}