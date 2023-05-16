using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "OnSwitchAbilities/New ProjectileOnSwitchAbilityData")]
public class ProjectileOnSwitchAbilityData : OnSwitchAbilityData
{
    public ProjectileData projectileData;
    public GameObject projectilePrefab;

    public override void Apply(GameObject player, IEnumerable<ModifierCount> modifiers)
    {
        var parent = GameObject.FindWithTag("ProjectileContainer").transform;
        var proj = Instantiate(projectilePrefab, player.transform.position, player.transform.rotation, parent);
        proj.GetComponent<Projectile>().Init(projectileData, modifiers);
    }
}