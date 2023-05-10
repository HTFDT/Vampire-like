using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New KnockBackModifierData")]
public class KnockBackModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnCollisionAction;
    public float power;
    public float recoveryTime;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        projectile.GetComponent<Projectile>().OnCollisionActions.AddLast(ApplyKnockBack);
    }

    private void ApplyKnockBack(Collision2D col, ChainNode<Collision2D> next)
    {
        next?.Action(col, next.Next);
        if (!col.gameObject.CompareTag("Enemy")) return;
        var script = col.gameObject.GetComponent<Enemy>();
        script.moveSpeed = 0;
        col.rigidbody.AddForce((col.rigidbody.position - col.otherRigidbody.position) * power);
        script.StartCoroutine(ReturnInitialMoveSpeed(script.initialMoveSpeed, script));
    }

    private IEnumerator ReturnInitialMoveSpeed(float initialMs, Enemy script)
    {
        yield return new WaitForSeconds(recoveryTime);
        script.moveSpeed = initialMs;
    }
}