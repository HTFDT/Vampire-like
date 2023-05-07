using System;
using Pathfinding;
using UnityEngine;


public class DefaultEnemyPathfinding : MonoBehaviour
{
    public float nextCheckpointDistance = .5f;
    private Transform _target;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    private Path _path;
    private int _currentCheckpoint;
    private bool _endOfPathReached;
    private Animator _animator;
    private Vector2 _moveDirection;
    private bool _facingRight;
    
    private void Awake()
    {
        _target = GameObject.FindWithTag("Player").transform;
        _seeker = gameObject.GetComponent<Seeker>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(StartPath), 0f, .5f);
    }

    private void StartPath()
    {
        if (_seeker.IsDone())
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentCheckpoint = 0;
        }
    }

    public void MakeStep(float speed)
    {
        if (_path == null)
            return;

        _endOfPathReached = _currentCheckpoint >= _path.vectorPath.Count;
        if (_endOfPathReached)
            return;

        var position = _rb.position;
        _moveDirection = ((Vector2)_path.vectorPath[_currentCheckpoint] - position).normalized;
        _rb.MovePosition(position + _moveDirection * (speed * Time.fixedDeltaTime));
        var distanceToNextCheckpoint = Vector2.Distance(_rb.position, _path.vectorPath[_currentCheckpoint]);
        if (distanceToNextCheckpoint < nextCheckpointDistance)
            _currentCheckpoint++;
    }
    
    private void Update()
    {
        var position = _rb.position;

        if (_moveDirection.x > 0 && !_facingRight || _moveDirection.x < 0 && _facingRight)
            Flip();
        
        _animator.SetBool("IsRunning", _moveDirection.magnitude > 0);
    }
    
    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}