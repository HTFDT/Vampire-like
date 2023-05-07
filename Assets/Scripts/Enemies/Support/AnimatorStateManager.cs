using System;
using UnityEngine;


public class AnimatorStateManager : MonoBehaviour
{
    private Animator _animator;
    public RuntimeAnimatorController controller;
    private Rigidbody2D _rb;
    private bool _facingRight;

    private void Awake()
    {
        _animator = gameObject.AddComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _animator.runtimeAnimatorController = controller;
    }
}