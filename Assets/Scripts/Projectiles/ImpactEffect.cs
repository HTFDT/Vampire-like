using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    public ImpactEffectData data;
    public Animator animator;
    
    public void Init(ImpactEffectData impactEffectData)
    {
        data = impactEffectData;
        animator.runtimeAnimatorController = impactEffectData.MainAnimatorController;
    }
}
