using System;
using UnityEngine;
using System.Drawing;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public float moveSpeed = 5f;
    public bool facingRight = true;
    public Animator animator;
    private PlayerInputActions _playerInputActions;
    private readonly int _isRunning = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Movement.Enable();
    }

    void Update()
    {
        moveDirection = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (moveDirection.x > 0 && !facingRight || moveDirection.x < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        moveDirection.Normalize();
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
        animator.SetBool(_isRunning, moveDirection.magnitude > 0);
    }

    private void Flip()
    {
        // var tr = transform;
        // var currentScale = tr.localScale;
        // currentScale.x *= -1;
        // tr.localScale = currentScale;
        
        facingRight = !facingRight;
        
        transform.Rotate(0f, 180f, 0f);
    }
}
