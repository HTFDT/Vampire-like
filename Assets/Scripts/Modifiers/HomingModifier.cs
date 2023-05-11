using System.Linq;
using UnityEngine;

namespace Modifiers
{
    [CreateAssetMenu(menuName = "Modifiers/New HomingModifier")]
    public class HomingModifier : ModifierData
    {
        public override ModifierTag Tag => ModifierTag.Initialization;
        public override void ApplyTo(GameObject projectile, int modifierCount)
        {
            projectile.GetComponent<Projectile>().StartActions.AddLast(RotateToEnemy);
        }

        private void RotateToEnemy(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0) return;
            var nearestEnemy = enemies.Aggregate((e1, e2) => GetNearest(rb.position, e1, e2));
            var direction = ((Vector2)nearestEnemy.transform.position - rb.position).normalized;
            rb.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
        
        private static GameObject GetNearest(Vector2 pos, GameObject e1, GameObject e2)
        {
            var dist1 = Vector2.Distance(e1.transform.position, pos);
            var dist2 = Vector2.Distance(e2.transform.position, pos);
            return dist1 < dist2 ? e1 : e2;
        }
    }
}