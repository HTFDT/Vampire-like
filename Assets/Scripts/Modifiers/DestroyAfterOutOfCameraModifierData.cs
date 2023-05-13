using UnityEngine;


[CreateAssetMenu(menuName = "Modifiers/New DestroyAfterOutOfCameraModifierData")]
public class DestroyAfterOutOfCameraModifierData : ModifierData
{
    public override ModifierTag Tag => ModifierTag.OnUpdateAction;
    public float distanceFromCameraBorder;

    public override void ApplyTo(GameObject projectile, int modifierCount)
    {
        var cameraProps = Camera.main!.GetComponent<CameraProps>();

        void DestroyIfOutOfCamera(Rigidbody2D rb, ChainNode<Rigidbody2D> next)
        {
            if (rb.position.y > cameraProps.Top + distanceFromCameraBorder || 
                rb.position.y < cameraProps.Bottom - distanceFromCameraBorder ||
                rb.position.x > cameraProps.Right + distanceFromCameraBorder ||
                rb.position.x < cameraProps.Left - distanceFromCameraBorder)
                Destroy(rb.gameObject);
            next?.Action(rb, next.Next);
        }

        projectile.GetComponent<Projectile>().UpdateActions.AddLast(DestroyIfOutOfCamera);
    }
}