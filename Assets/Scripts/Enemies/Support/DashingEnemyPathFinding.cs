using System;
using UnityEngine;


public class DashingEnemyPathFinding : DefaultEnemyPathfinding
{
    private float _dashTime;
    private Action _onDashEnd;
    public bool IsDashing => _dashTime > 0;
    public void Dash(float dashSpeed)
    {
        Animator.SetBool("IsDashing", true);
        Rb.MovePosition(Rb.position + MoveDirection * (dashSpeed * Time.fixedDeltaTime));
        _dashTime -= Time.fixedDeltaTime;
        if (IsDashing) return;
        Animator.SetBool("IsDashing", false);
        _onDashEnd();
    }

    public void SetDashState(float dashFullTime, Action onDashEnd)
    {
        _dashTime = dashFullTime;
        _onDashEnd = onDashEnd;
    }
}