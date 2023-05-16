using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New ProjectileSynergyModifierData")]
public class ProjectileSynergyModifierData : SynergyModifierData
{
    public ProjectileData projectileToSpawn;
    public GameObject projectilePrefab;
    
    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        var script = projectile.GetComponent<Projectile>();
        var projectileContainer = GameObject.FindWithTag("ProjectileContainer").transform;
        script.OnCollisionActions.AddLast(SpawnProjectileOnCollision);

        void SpawnProjectileOnCollision(Collision2D col, ChainNode<Collision2D> next)
        {
            next?.Action(col, next.Next);
            if (!col.gameObject.CompareTag("Enemy")) return;
            var cont = col.gameObject.GetComponent<BuffContainer>();
            if (cont == null) return;
            if (cont.BuffDataToDuration.Keys.All(buff => buff.attackType != withAttackType)) return;
            var contact = col.GetContact(0);
            var proj = Instantiate(projectilePrefab, contact.point, script.transform.rotation, projectileContainer);
            proj.SetActive(false);
            proj.GetComponent<Projectile>().Init(projectileToSpawn, Enumerable.Empty<ModifierCount>());
            proj.SetActive(true);
        }
    }
}