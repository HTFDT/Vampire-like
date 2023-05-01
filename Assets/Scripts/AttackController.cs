using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// эта хуйня отвечает за считывание переключения форм. Т.е. она должна запускать корутины для генерации снарядов и применять к ним все модификаторы.
public class AttackController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public GameObject projectileContainer;
    public List<ProjectileData> projectileDataList;

    private void Start()
    {
        StartCoroutine(LaunchAttack());
    }

    private IEnumerator LaunchAttack()
    {
        while (true)
        {
            var proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, projectileContainer.transform);
            proj.GetComponent<Projectile>().Init(projectileDataList[0]);
            yield return new WaitForSeconds(1);
        }
    }
}