using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Modifiers/New BuffModifierData")]
public class BuffModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;
    public BuffData buffData;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        base.ApplyTo(projectile, modifierCount);
        projectile.GetComponent<Projectile>().OnCollisionActions.AddLast(ApplyBuff);
    }

    private void ApplyBuff(Collision2D col, ChainNode<Collision2D> next)
    {
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (!col.gameObject.TryGetComponent(out BuffContainer cont))
                cont = col.gameObject.AddComponent<BuffContainer>();
            cont.ApplyBuff(buffData);
        }
        next?.Action.Invoke(col, next.Next);
    }
}