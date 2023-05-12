using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Projectiles/New SelfInstantiatingProjectile")]
public class SelfInstantiatingProjectileData : ProjectileData
{
    public GameObject projectilePrefab;
    public float delayBeforeNextSegmentSpawn;

    public override void StartActions(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        base.StartActions(rb, next);
        var camProps = Camera.main!.GetComponent<CameraProps>();
        var script = rb.gameObject.GetComponent<Projectile>();
        if (rb.position.x < camProps.Left || rb.position.x > camProps.Right) return;
        script.StartCoroutine(SpawnNextSegment(rb, script));
    }

    private IEnumerator SpawnNextSegment(Rigidbody2D rb, Projectile script)
    {
        yield return new WaitForSeconds(delayBeforeNextSegmentSpawn);
        var transform = rb.transform;
        var spriteRenderer = rb.gameObject.GetComponent<SpriteRenderer>();
        var proj = Instantiate(projectilePrefab, rb.position + (Vector2)(transform.right * (spriteRenderer.size.x * .5f)),
            transform.rotation);
        proj.SetActive(false);
        var sc = proj.GetComponent<Projectile>();
        sc.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        sc.Init(script.data, script.AdditionalModifiers);
        proj.SetActive(true);
    }
}