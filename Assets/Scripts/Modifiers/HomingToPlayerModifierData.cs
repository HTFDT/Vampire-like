using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New HomingToPlayerModifierData")]
public class HomingToPlayerModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.Initialization;
    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Projectile>().StartActions.AddLast(RotateToPlayer);
    }
    
    private void RotateToPlayer(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
    {
        next?.Action(rb, next.Next);
        var player = GameObject.FindWithTag("Player").transform;
        var direction = ((Vector2)player.position - rb.position).normalized;
        rb.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}