using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public abstract class BuffData : ScriptableObject
{
    public AttackTypesEnum attackType;
    public float duration;

    public abstract IEnumerator Buff(GameObject obj, Func<bool> predicate, Action onDurationEnd);

}