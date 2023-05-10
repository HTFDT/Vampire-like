using System;
using Unity.VisualScripting;
using UnityEngine;


public class ApplyDebuffOnCollision : MonoBehaviour
{
    public BuffData debuff;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Enemy")) return;
        if (!col.gameObject.TryGetComponent<BuffContainer>(out var cont))
            cont = col.gameObject.AddComponent<BuffContainer>();
        cont.ApplyBuff(debuff);
    }
}