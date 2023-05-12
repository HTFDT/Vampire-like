using System;
using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Buffs/New TickingDamageDebuffData")]
public class TickingDamageDebuffData : BuffData
{
    public float burnDamage;
    public float delayInSeconds;
    public GameObject burningEffectPrefab;

    public override IEnumerator Buff(GameObject obj, Func<bool> predicate, Action onDurationEnd)
    {
        var enemy = obj.GetComponent<Enemy>();
        var effect = SpawnFireEffect(enemy);
        var animator = effect.GetComponent<Animator>();

        while (predicate())
        {
            enemy.TakeDamage(burnDamage);
            yield return new WaitForSeconds(delayInSeconds);
        }
        
        animator.SetBool("IsEnded", true);
        onDurationEnd();
    }

    private GameObject SpawnFireEffect(Enemy enemyScript)
    {
        var effect = Instantiate(burningEffectPrefab, enemyScript.rb.position, enemyScript.transform.rotation,
            enemyScript.rb.transform);
        return effect;
    }
}