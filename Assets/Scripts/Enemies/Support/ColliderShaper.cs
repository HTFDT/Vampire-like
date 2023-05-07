using System;
using UnityEngine;


public class ColliderShaper : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    private SpriteRenderer _renderer;
    private void Awake()
    {
        _collider = gameObject.AddComponent<CapsuleCollider2D>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _collider.size = _renderer.size;
    }
}