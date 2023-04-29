using UnityEngine;
using System.Drawing;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public float moveSpeed = 5f;
    public bool facingRight = true;
    public Animator animator;
    private readonly int _isRunning = Animator.StringToHash("IsRunning");

    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

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
