using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New BuffSynergyModifierData")]
public class BuffSynergyModifierData : SynergyModifierData
{
    public BuffData buffToApply;
    
    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Projectile>().OnCollisionActions.AddLast(ApplyOnCondition);
    }
    
    private void ApplyOnCondition(Collision2D col, ChainNode<Collision2D> next)
    {
        next?.Action(col, next.Next);
        if (!col.gameObject.CompareTag("Enemy")) return;
        var cont = col.gameObject.GetComponent<BuffContainer>();
        if (cont == null) return;
        if (cont.BuffDataToDuration.Keys.Any(buffData => buffData.attackType == withAttackType))
            cont.ApplyBuff(buffToApply);
    }
}