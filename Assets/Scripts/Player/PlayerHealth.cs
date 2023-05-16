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
    public bool canTakeDamage = true;
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
        if (canTakeDamage)
        {
            health -= dmg;
            canTakeDamage = false;
            StartCoroutine(WaitUntilInvincibilityTimePass());
            if (health > 0) return;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Animator>().SetBool(IsDead, true);
        }
        healthBar.SetHealth(health, _maxHealth);
    }

    private IEnumerator WaitUntilInvincibilityTimePass()
    {
        yield return new WaitForSeconds(invincibilityTimeInSeconds);
        canTakeDamage = true;
    }
}
