using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int moveSpeed;

    public RuntimeAnimatorController AnimatorController => animatorController;
    public int Health => health;
    public int Damage => damage;
    public int MoveSpeed => moveSpeed;

    public virtual void ApplyTo(GameObject enemyObj)
    {
        enemyObj.AddComponent<Animator>().runtimeAnimatorController = animatorController;
        var spriteRenderer = enemyObj.GetComponent<SpriteRenderer>();
        enemyObj.AddComponent<BoxCollider2D>().size = spriteRenderer.size;
    }
}
