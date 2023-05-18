using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{
    [Serializable]
    public class CountdownOutline
    {
        public AttackTypesEnum attackType;
        public Outline outline;
        public Countdown countdown;
    }
    public List<CountdownOutline> countdownOutlines;
    public AudioSource cooldownEndSound;
    
    public void SetActiveAttackType(AttackTypesEnum activeAttackType, float cooldownTime)
    {
        countdownOutlines.Single(co => co.attackType == activeAttackType).outline.enabled = true;
        foreach (var co in countdownOutlines.Where(x => x.attackType != activeAttackType))
        {
            co.outline.enabled = false;
            co.countdown.SetCountdown(cooldownTime);
        }
        cooldownEndSound.PlayDelayed(cooldownTime);
    }
}