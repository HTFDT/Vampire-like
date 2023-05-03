using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New AnimatorModifierData")]
public class AnimatorModifierData : ModifierData
{
    [SerializeField] private RuntimeAnimatorController mainAnimatorController;

    public override void ApplyTo(GameObject proj)
    {
        base.ApplyTo(proj);
        proj.AddComponent<Animator>().runtimeAnimatorController = mainAnimatorController;
    }
}