using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffContainer : MonoBehaviour
{
    public readonly Dictionary<BuffData, float> BuffDataToDuration = new();

    public void ApplyBuff(BuffData buffData)
    {
        if (!BuffDataToDuration.ContainsKey(buffData))
        {
            BuffDataToDuration[buffData] = buffData.duration;
            StartCoroutine(buffData.Buff(gameObject, () => BuffDataToDuration[buffData] > 0,
                () => BuffDataToDuration.Remove(buffData)));
            return;
        }
        BuffDataToDuration[buffData] = buffData.duration;
    }

    private void Update()
    {
        var buffs = BuffDataToDuration.Keys.ToArray();
        foreach (var buff in buffs)
            BuffDataToDuration[buff] -= Time.deltaTime;
    }
}