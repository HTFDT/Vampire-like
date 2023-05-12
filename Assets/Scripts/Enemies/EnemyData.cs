using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;

    public RuntimeAnimatorController AnimatorController => animatorController;
    public float Health => health;
    public float Damage => damage;
    public float MoveSpeed => moveSpeed;

    public virtual void ApplyTo(GameObject enemyObj)
    {
        enemyObj.AddComponent<AnimatorStateManager>().controller = animatorController;
        enemyObj.AddComponent<ColliderShaper>();
        enemyObj.GetComponent<Enemy>().HandleDamage = HandleDamage;
    }

    protected virtual void HandleDamage(float dmg, GameObject enemy)
    {
        var script = enemy.GetComponent<Enemy>();
        script.health -= dmg;
        if (!(script.health <= 0)) return;
        enemy.GetComponent<Animator>().SetBool("IsDead", true);
        script.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        enemy.GetComponent<Collider2D>().enabled = false;
    }
}
