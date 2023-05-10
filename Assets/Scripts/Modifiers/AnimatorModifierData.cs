using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New AnimatorModifierData")]
public class AnimatorModifierData : ModifierData
{
    [SerializeField] private RuntimeAnimatorController mainAnimatorController;

    public override ModifierTag Tag => ModifierTag.Initialization;

    public override void ApplyTo(GameObject proj, int modifierCount)
    {
        proj.AddComponent<Animator>().runtimeAnimatorController = mainAnimatorController;
    }

    
}