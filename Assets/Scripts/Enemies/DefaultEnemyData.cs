using UnityEngine;


[CreateAssetMenu(menuName = "Enemies/New DefaultEnemyData")]
public class DefaultEnemyData : EnemyData
{
    public override void ApplyTo(GameObject enemyObj)
    {
        base.ApplyTo(enemyObj);
        var pathfindingScript = enemyObj.AddComponent<DefaultEnemyPathfinding>();
        var script = enemyObj.GetComponent<Enemy>();
        script.UpdateActions += rb => pathfindingScript.MakeStep(MoveSpeed); }
}