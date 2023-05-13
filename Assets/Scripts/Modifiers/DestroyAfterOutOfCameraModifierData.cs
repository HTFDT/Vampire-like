using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New DestroyAfterOutOfCameraModifierData")]
public class DestroyAfterOutOfCameraModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnUpdateAction;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        var cameraProps = Camera.main!.GetComponent<CameraProps>();

        void DestroyIfOutOfCamera(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
        {
            if (!cameraProps.InBounds(rb.position))
                Destroy(rb.gameObject);
            next?.Action(rb, next.Next);
        }

        projectile.GetComponent<Projectile>().UpdateActions.AddLast(DestroyIfOutOfCamera);
    }
}