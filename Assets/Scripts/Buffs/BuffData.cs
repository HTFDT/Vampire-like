using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public abstract class BuffData : ScriptableObject
{
    public abstract BuffAttackType BuffType { get; }
    public float duration;

    public abstract IEnumerator Buff(GameObject obj, Func<bool> predicate, Action onDurationEnd);

}