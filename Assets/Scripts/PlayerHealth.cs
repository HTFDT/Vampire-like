using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public float health;
    private float _maxHealth;
    public float invincibilityTimeInSeconds;
    private bool _canTakeDamage = true;
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    private void Awake()
    {
        _maxHealth = health;
    }

    private void Start()
    {
        healthBar.SetHealth(health, _maxHealth);
    }

    public void TakeDamage(float dmg)
    {
        if (_canTakeDamage)
        {
            health -= dmg;
            if (health <= 0)
                gameObject.GetComponent<Animator>().SetBool(IsDead, true);
            _canTakeDamage = false;
            Invoke(nameof(WaitUntilInvincibilityTimePass), invincibilityTimeInSeconds);
        }
        healthBar.SetHealth(health, _maxHealth);
    }

    private void WaitUntilInvincibilityTimePass()
    {
        _canTakeDamage = true;
    }
}
