using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class AttackController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public GameObject projectileContainer;
    public float switchCooldown;
    public List<AttackTypeManager> attackTypes;
    private Dictionary<Key, AttackTypesEnum> _keyToAttackType;
    private AttackTypesEnum _currentAttackType;
    private Coroutine _currentCoroutine;
    private bool _switchOnCooldown;
    private PlayerInputActions _actions;
    public Dictionary<AttackTypesEnum, AttackTypeManager> AttackTypeToManager;

    [Serializable]
    public class AttackTypeManager
    {
        public List<ModifierCount> modifiers;
        public ProjectileData projectile;
        public float attackDelay;
        public AttackTypesEnum attackType;
        public OnSwitchAbilityData onSwitchAbility;
    }


    private void Awake()
    {
        _actions = new PlayerInputActions();
        _actions.Player.SwitchAttackType.Enable();
        _actions.Player.SwitchAttackType.performed += SwitchAttackType;
        _keyToAttackType = new Dictionary<Key, AttackTypesEnum>
        {
            [Key.Q] = AttackTypesEnum.Fire,
            [Key.W] = AttackTypesEnum.Air,
            [Key.E] = AttackTypesEnum.Water,
            [Key.R] = AttackTypesEnum.Earth
        };
        _currentAttackType = _keyToAttackType[Key.Q];

        AttackTypeToManager = new Dictionary<AttackTypesEnum, AttackTypeManager>();
        foreach (var manager in attackTypes)
            AttackTypeToManager[manager.attackType] = manager;
    }

    private void Start()
    {
        _currentCoroutine = StartCoroutine(LaunchAttackCycle());
    }

    private void SwitchAttackType(InputAction.CallbackContext context)
    {
        if (_switchOnCooldown) return;
        var typeToSwitch = _keyToAttackType[(context.control as KeyControl)!.keyCode];
        if (_currentAttackType == typeToSwitch) return;
        _currentAttackType = typeToSwitch;
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(LaunchAttackCycle());
        _switchOnCooldown = true;
        Invoke(nameof(EndCooldown), switchCooldown);
    }

    private void EndCooldown()
    {
        _switchOnCooldown = false;
    }

    private IEnumerator LaunchAttackCycle()
    {
        AttackTypeToManager[_currentAttackType].onSwitchAbility.Apply(gameObject, AttackTypeToManager[_currentAttackType].modifiers);
        while (true)
        {
            var data = AttackTypeToManager[_currentAttackType].projectile;
            var proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation,
                projectileContainer.transform);
            proj.SetActive(false);
            proj.GetComponent<Projectile>().Init(data, AttackTypeToManager[_currentAttackType].modifiers);
            proj.SetActive(true);


            yield return new WaitForSeconds(AttackTypeToManager[_currentAttackType].attackDelay);
        }
    }

    private void OnDestroy()
    {
        _actions.Player.SwitchAttackType.performed -= SwitchAttackType;
    }
}