using System.Collections;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemies/New ShootingEnemyData")]
public class ShootingEnemyData : DefaultEnemyData
{
    public float shootingDistance;
    public GameObject projectilePrefab;
    public ProjectileData projectile;
    public float delayInSeconds;
    public float delayBeforeStartShootingInSeconds;

    public override void ApplyTo(GameObject enemyObj)
    {
        base.ApplyTo(enemyObj);
        var script = enemyObj.GetComponent<Enemy>();
        var pathfindingScript = enemyObj.GetComponent<DefaultEnemyPathfinding>();
        var projectileContainer = GameObject.FindWithTag("ProjectileContainer").transform;
        Coroutine projRoutine = null;
        var isShooting = false;
        script.UpdateActions = ShootingEnemyMovement;

        void ShootingEnemyMovement(Rigidbody2D rb)
        {
            if (Vector2.Distance(pathfindingScript.target.position, rb.position) > shootingDistance)
            {
                script.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                script.moveSpeed = script.initialMoveSpeed;
                pathfindingScript.MakeStep(script.moveSpeed);
                if (projRoutine == null) return;
                script.StopCoroutine(projRoutine);
                isShooting = false;
            }
            else
            {
                script.rb.constraints = RigidbodyConstraints2D.FreezeAll;
                script.moveSpeed = 0;
                if (isShooting) return;
                projRoutine = script.StartCoroutine(SpawnProjectileCoroutine(rb));
                isShooting = true;
            }
        }

        IEnumerator SpawnProjectileCoroutine(Rigidbody2D rb)
        {
            yield return new WaitForSeconds(delayBeforeStartShootingInSeconds);
            while (true)
            {
                var proj = Instantiate(projectilePrefab, rb.position, rb.transform.rotation, projectileContainer);
                proj.SetActive(false);
                var projScript = proj.GetComponent<Projectile>();
                projScript.Init(projectile, Enumerable.Empty<ModifierCount>());
                proj.SetActive(true);
                yield return new WaitForSeconds(delayInSeconds);
            }
        }
    }
}