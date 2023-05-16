using System;
using System.Collections;
using UnityEngine;

namespace Buffs
{
    [CreateAssetMenu(menuName = "Buffs/New MoveSpeedStatusEffectBuffData")]
    public class MoveSpeedStatusEffectBuffData : BuffData
    {
        public float decrement;
        public Color spriteColor;
        public override IEnumerator Buff(GameObject obj, Func<bool> predicate, Action onDurationEnd)
        {
            var script = obj.GetComponent<Enemy>();
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            spriteRenderer.color = spriteColor;
            script.moveSpeed -= script.moveSpeed * decrement;
            yield return new WaitWhile(predicate);
            script.moveSpeed = script.initialMoveSpeed;
            spriteRenderer.color = script.initialColor;
            onDurationEnd();
        }
    }
}