using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New ImpactEffectModifierData")]
public class ImpactEffectModifierData : ModifierData
{
    [SerializeField] private GameObject impactEffectPrefab;
    [SerializeField] private ImpactEffectData impactEffectData;

    public override void ApplyTo(GameObject proj)
    {
        base.ApplyTo(proj);
        proj.GetComponent<Projectile>().OnCollisionActions += InstantiateOnCollision;
    }

    private void InstantiateOnCollision(Collision2D col)
    {
        var obj = Instantiate(impactEffectPrefab, col.otherRigidbody.position, col.otherRigidbody.transform.rotation);
        var impactEffect = obj.GetComponent<ImpactEffect>();
        impactEffect.Init(impactEffectData);
    }
}