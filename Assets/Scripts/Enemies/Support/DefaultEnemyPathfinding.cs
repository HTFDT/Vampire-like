using System;
using Pathfinding;
using UnityEngine;


public class DefaultEnemyPathfinding : MonoBehaviour
{
    public float nextCheckpointDistance = .5f;
    public Transform target;
    protected Seeker Seeker;
    protected Rigidbody2D Rb;
    protected Path Path;
    protected int CurrentCheckpoint;
    protected bool EndOfPathReached;
    protected Animator Animator;
    protected Vector2 MoveDirection;
    protected bool FacingRight = true;
    
    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        Seeker = gameObject.GetComponent<Seeker>();
        Rb = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(StartPath), 0f, .5f);
    }

    private void StartPath()
    {
        if (Seeker.IsDone())
            Seeker.StartPath(Rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            Path = path;
            CurrentCheckpoint = 0;
        }
    }

    public void MakeStep(float speed)
    {
        if (Path == null)
            return;

        EndOfPathReached = CurrentCheckpoint >= Path.vectorPath.Count;
        if (EndOfPathReached)
            return;

        var position = Rb.position;
        MoveDirection = ((Vector2)Path.vectorPath[CurrentCheckpoint] - position).normalized;
        Rb.MovePosition(position + MoveDirection * (speed * Time.fixedDeltaTime));
        var distanceToNextCheckpoint = Vector2.Distance(Rb.position, Path.vectorPath[CurrentCheckpoint]);
        if (distanceToNextCheckpoint < nextCheckpointDistance)
            CurrentCheckpoint++;
    }
    
    private void Update()
    {
        if (MoveDirection.x > .2f && !FacingRight || MoveDirection.x < -.2f && FacingRight)
            Flip();
        
        Animator.SetBool("IsRunning", MoveDirection.magnitude > .1f);
    }
    
    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}