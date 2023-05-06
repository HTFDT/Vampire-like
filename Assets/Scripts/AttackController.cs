using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


// эта хуйня отвечает за считывание переключения форм. Т.е. она должна запускать корутины для генерации снарядов и применять к ним все модификаторы.
public class AttackController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public GameObject projectileContainer;
    public List<AttackTypeData> attackTypes; 
    private Dictionary<Key, AttackTypeManager> _keyToAttackType;
    private AttackTypeManager _currentAttackType;
    private Coroutine _currentCoroutine;


    private void Awake()
    {
        var actions = new PlayerInputActions();
        actions.Player.SwitchAttackType.Enable();
        actions.Player.SwitchAttackType.performed += SwitchAttackType;
        _keyToAttackType = new Dictionary<Key, AttackTypeManager>
        {
            [Key.Q] = new(attackTypes[0]),
            [Key.W] = new(attackTypes[1]),
            [Key.E] = new(attackTypes[2]),
            [Key.R] = new(attackTypes[3])
        };
        _currentAttackType = _keyToAttackType[Key.Q];
    }

    private void Start()
    {
        _currentCoroutine = StartCoroutine(_currentAttackType.LaunchCoroutine(firePoint, projectilePrefab, projectileContainer,
            _currentAttackType.Projectiles, _currentAttackType.AttackDelay, _currentAttackType.Modifiers));
    }

    private void SwitchAttackType(InputAction.CallbackContext context)
    {
        _currentAttackType = _keyToAttackType[(context.control as KeyControl)!.keyCode];
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(_currentAttackType.LaunchCoroutine(firePoint, projectilePrefab, projectileContainer,
            _currentAttackType.Projectiles, _currentAttackType.AttackDelay, _currentAttackType.Modifiers));
    }
}