using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Attack Types/New AttackTypeData")]
public class AttackTypeData : ScriptableObject
{
    [SerializeField] private List<ProjectileData> defaultProjectiles;
    [Tooltip("Modifiers added to all the projectiles in attack")]
    [SerializeField] private List<ModifierCount> defaultModifiers;
    [SerializeField] private float defaultAttackDelay;

    public List<ProjectileData> DefaultProjectiles => defaultProjectiles;
    public List<ModifierCount> DefaultModifiers => defaultModifiers;
    public float DefaultAttackDelay => defaultAttackDelay;

    public IEnumerator LaunchAttackCycle(Transform firePoint,
        GameObject projectilePrefab, 
        GameObject projectileContainer,
        List<ProjectileData> projectiles,
        float attackDelay,
        List<ModifierCount> mods)
    {
        while (true)
        {
            foreach (var data in projectiles)
            {
                var proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation,
                    projectileContainer.transform);
                proj.GetComponent<Projectile>().Init(data);
                foreach (var mod in mods.Concat(data.BaseModifiers).OrderBy(mod => (mod.modifier.Tag, mod.modifier.weight)))
                    mod.modifier.ApplyTo(proj, mod.count);
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }
}
