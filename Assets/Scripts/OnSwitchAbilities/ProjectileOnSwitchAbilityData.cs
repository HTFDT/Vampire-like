using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "OnSwitchAbilities/New ProjectileOnSwitchAbilityData")]
public class ProjectileOnSwitchAbilityData : OnSwitchAbilityData
{
    public ProjectileData projectileData;
    public GameObject projectilePrefab;
    public List<ModifierCount> modifiers;
    
    public override void Apply(GameObject player)
    {
        var proj = Instantiate(projectilePrefab, player.transform.position, player.transform.rotation, player.transform);
        proj.GetComponent<Projectile>().Init(projectileData, modifiers);
    }
}