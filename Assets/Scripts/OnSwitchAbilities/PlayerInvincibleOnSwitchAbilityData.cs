using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "OnSwitchAbilities/New PlayerInvincibleOnSwitchAbilityData")]
public class PlayerInvincibleOnSwitchAbilityData : OnSwitchAbilityData
{
    public Color spriteColor;
    public float duration;

    public override void Apply(GameObject player)
    {
        var script = player.GetComponent<PlayerHealth>();
        script.StopAllCoroutines();
        script.canTakeDamage = false;
        var spriteRenderer = player.GetComponent<SpriteRenderer>();
        var initialColor = spriteRenderer.color;
        spriteRenderer.color = spriteColor;
        script.StartCoroutine(ResetInvincibility(script, spriteRenderer, initialColor));
    }

    private IEnumerator ResetInvincibility(PlayerHealth script, SpriteRenderer spriteRenderer, Color color)
    {
        yield return new WaitForSeconds(duration);
        script.canTakeDamage = true;
        spriteRenderer.color = color;
    }
}