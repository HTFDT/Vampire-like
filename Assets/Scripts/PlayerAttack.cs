using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePoint;
    public float attackDelay = 1f;
    public GameObject projectile;
    public GameObject projectileContainer;

    void Start()
    {
        StartCoroutine(SpawnProjectile());
    }

    private IEnumerator SpawnProjectile()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(attackDelay);
        }
    }

    private void Shoot()
    {
        var pr = Instantiate(projectile, firePoint.position, firePoint.rotation);
        pr.transform.parent = projectileContainer.transform;
    }
}
