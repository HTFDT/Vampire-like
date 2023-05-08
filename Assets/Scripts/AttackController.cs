using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


// эта хуйня отвечает за считывание переключения форм. Т.е. она должна запускать корутины для генерации снарядов и применять к ним все модификаторы.
public class AttackController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public GameObject projectileContainer;
    public List<AttackTypeManager> attackTypes;
    private Dictionary<Key, AttackTypeManager> _keyToAttackType;
    private AttackTypeManager _currentAttackType;
    private Coroutine _currentCoroutine;

    [Serializable]
    public class AttackTypeManager
    {
        public List<ModifierCount> modifiers;
        public List<ProjectileData> projectiles;
        public float attackDelay;
    }


    private void Awake()
    {
        var actions = new PlayerInputActions();
        actions.Player.SwitchAttackType.Enable();
        actions.Player.SwitchAttackType.performed += SwitchAttackType;
        _keyToAttackType = new Dictionary<Key, AttackTypeManager>
        {
            [Key.Q] = attackTypes[0],
            [Key.W] = attackTypes[1],
            [Key.E] = attackTypes[2],
            [Key.R] = attackTypes[3]
        };
        _currentAttackType = _keyToAttackType[Key.Q];
    }

    private void Start()
    {
        _currentCoroutine = StartCoroutine(LaunchAttackCycle());
    }

    private void SwitchAttackType(InputAction.CallbackContext context)
    {
        _currentAttackType = _keyToAttackType[(context.control as KeyControl)!.keyCode];
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(LaunchAttackCycle());
    }

    public IEnumerator LaunchAttackCycle()
    {
        while (true)
        {
            foreach (var data in _currentAttackType.projectiles)
            {
                var proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation,
                    projectileContainer.transform);
                proj.GetComponent<Projectile>().Init(data);
                foreach (var mod in _currentAttackType.modifiers.Concat(data.BaseModifiers)
                             .OrderBy(mod => (mod.modifier.Tag, mod.modifier.weight)))
                    mod.modifier.ApplyTo(proj, mod.count);
            }

            yield return new WaitForSeconds(_currentAttackType.attackDelay);
        }
    }
}