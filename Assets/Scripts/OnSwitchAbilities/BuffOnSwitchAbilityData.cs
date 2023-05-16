using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "OnSwitchAbilities/New BuffOnSwitchAbilityData")]
public class BuffOnSwitchAbilityData : OnSwitchAbilityData
{
      public float radius;
      public BuffData debuff;
      public float timeBeforeColliderDestroying;

      public override void Apply(GameObject player, IEnumerable<ModifierCount> modifiers)
      {
          var obj = new GameObject();
          obj.SetActive(false);
          obj.transform.parent = player.transform;
          obj.transform.position = player.transform.position;
          obj.AddComponent<Rigidbody2D>().gravityScale = 0;
          var collider = obj.AddComponent<CircleCollider2D>();
          collider.isTrigger = true;
          collider.radius = radius;
          obj.AddComponent<ApplyDebuffOnCollision>().debuff = debuff;
          obj.SetActive(true);
          Destroy(obj, timeBeforeColliderDestroying);
      }
}