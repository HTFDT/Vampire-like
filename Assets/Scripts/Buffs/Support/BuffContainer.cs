using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffContainer : MonoBehaviour
{
    private Dictionary<BuffData, float> _buffDataToDuration = new();

    public void ApplyBuff(BuffData buffData)
    {
        _buffDataToDuration[buffData] = buffData.duration;
        StartCoroutine(buffData.Buff(gameObject, () => _buffDataToDuration[buffData] > 0,
            () => _buffDataToDuration.Remove(buffData)));
    }

    private void Update()
    {
        var buffs = _buffDataToDuration.Keys.ToArray();
        foreach (var buff in buffs)
            _buffDataToDuration[buff] -= Time.deltaTime;
    }
}