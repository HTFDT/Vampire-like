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
    }
}
