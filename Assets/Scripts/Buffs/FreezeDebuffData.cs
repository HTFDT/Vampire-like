using System;
using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Buffs/New FreezeDebuffData")]
public class FreezeDebuffData : BuffData
{
    public Color frozenColor;

    public override IEnumerator Buff(GameObject obj, Func<bool> predicate, Action onDurationEnd)
    {
        var script = obj.GetComponent<Enemy>();
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        var animator = obj.GetComponent<Animator>();
        animator.enabled = false;
        spriteRenderer.color = frozenColor;
        script.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitWhile(predicate);
        spriteRenderer.color = script.initialColor;
        animator.enabled = true;
        script.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        onDurationEnd();
    }
}