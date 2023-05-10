using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New ImpactEffectModifierData")]
public class ImpactEffectModifierData : ModifierData
{
    [SerializeField] private GameObject impactEffectPrefab;
    [SerializeField] private ImpactEffectData impactEffectData;

    public override ModifierTag Tag => ModifierTag.OnCollisionAction;

    public override void ApplyTo(GameObject proj, int modifierCount)
    {
        proj.GetComponent<Projectile>().OnCollisionActions.AddLast(InstantiateOnCollision);
    }

    private void InstantiateOnCollision(Collision2D col, ChainNode<Collision2D> next)
    {
        var contact = col.GetContact(0);
        var obj = Instantiate(impactEffectPrefab, contact.point, col.otherRigidbody.transform.rotation);
        var impactEffect = obj.GetComponent<ImpactEffect>();
        impactEffect.Init(impactEffectData);
        next?.Action(col, next.Next);
    }
}