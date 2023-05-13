using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemies/New DashingEnemyData")]
public class DashingEnemyData : EnemyData
{
    public float dashDistance;
    public float dashSpeed;
    public float dashTimeInSeconds;
    public float dashCooldownInSeconds;
    
    public override void ApplyTo(GameObject enemyObj)
    {
        base.ApplyTo(enemyObj);
        var pathfindingScript = enemyObj.AddComponent<DashingEnemyPathFinding>();
        var script = enemyObj.GetComponent<Enemy>();
        var camProps = Camera.main!.GetComponent<CameraProps>();
        var canDash = true;
        script.UpdateActions += DashMovement;

        void DashMovement(Rigidbody2D rb)
        {
            if (!pathfindingScript.IsDashing && canDash && camProps.InBounds(rb.position) &&
                Vector2.Distance(pathfindingScript.target.position, rb.position) <= dashDistance)
            {
                script.moveSpeed = dashSpeed;
                pathfindingScript.SetDashState(dashTimeInSeconds, () =>
                {
                    script.moveSpeed = script.initialMoveSpeed;
                    script.StartCoroutine(SetCooldown());
                });
            }
            
            if (pathfindingScript.IsDashing)
                pathfindingScript.Dash(script.moveSpeed);
            else
                pathfindingScript.MakeStep(script.moveSpeed);
        }
        
        IEnumerator SetCooldown()
        {
            canDash = false;
            yield return new WaitForSeconds(dashCooldownInSeconds);
            canDash = true;
        }
    }
}