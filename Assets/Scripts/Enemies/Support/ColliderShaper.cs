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
        if (_renderer.sprite == null) return;
        var sprite = _renderer.sprite;
        _collider.direction = sprite.rect.size.x > sprite.rect.size.y ? CapsuleDirection2D.Horizontal : CapsuleDirection2D.Vertical;
        _collider.size = sprite.rect.size / sprite.pixelsPerUnit;
    }
}