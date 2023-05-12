using System.Collections;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New AdditionalProjectileModifierData")]
public class AdditionalProjectileModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.Initialization;
    public ProjectileData additionalProjectile;
    public GameObject projectilePrefab;
    public float delayCorrelationFromManagerDelay;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        var attackController = GameObject.FindWithTag("Player")
            .GetComponent<AttackController>();
        var manager = attackController
            .attackTypes.Single(manager => manager.attackType == attackType);
        var script = projectile.GetComponent<Projectile>();
        var projectileContainer = GameObject.FindWithTag("ProjectileContainer").transform;
        script.StartActions.AddLast((rb, next) =>
        {
            script.StartCoroutine(SpawnAdditionalProjectile());
            next?.Action(rb, next.Next);
        });


        IEnumerator SpawnAdditionalProjectile()
        {
            for (var i = 0; i < modifierCount; i++)
            {
                yield return new WaitForSeconds(manager.attackDelay * delayCorrelationFromManagerDelay);
                var proj = Instantiate(projectilePrefab, attackController.firePoint.position,
                    attackController.firePoint.rotation, projectileContainer);
                proj.SetActive(false);
                proj.GetComponent<Projectile>().Init(additionalProjectile,
                    script.AdditionalModifiers.Where(cnt => cnt.modifier is not AdditionalProjectileModifierData));
                proj.SetActive(true);
            }
        }
    }
}