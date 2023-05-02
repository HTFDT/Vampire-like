using UnityEngine;

[CreateAssetMenu(menuName = "Impact Effects/New ImpactEffectData")]
public class ImpactEffectData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController mainAnimatorController;
    public RuntimeAnimatorController MainAnimatorController => mainAnimatorController;
}