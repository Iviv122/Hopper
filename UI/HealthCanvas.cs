using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthCanvas : MonoBehaviour
{
    [SerializeField] PlayerInfo player;
    [SerializeField] private Image redSplatterImage = null;
    [SerializeField] private Image hurtImage;
    [SerializeField] private float hurtTimer = 0.4f;
    [SerializeField] private float a;
    void Awake()
    {
        player.HealthChanged+=UpdateHealth;
        player.DamageTaken += OnDamageTaken;
        hurtImage.enabled = false;

        a = hurtImage.color.a;
    }

    void UpdateHealth(){
        if(player.Health <= 0) return;
        Color splatterAlpha = new Color(1,0,0,1-((float)player.Health/player.MaxHealth*2));
        //Debug.Log(splatterAlpha);
        redSplatterImage.color = splatterAlpha;
    }
    
    private void OnDamageTaken()
    {
        Debug.Log("DamageTaken");
        StartCoroutine(HurtFlash());
    }
    IEnumerator HurtFlash()
    {
        if (hurtImage == null) yield break;

        hurtImage.enabled = true;

        // Ensure image color has full opacity when showing
        Color tempColor = hurtImage.color;
        tempColor.a = a;
        hurtImage.color = tempColor;

        // Fade out smoothly instead of instantly disappearing
        float fadeDuration = hurtTimer;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            tempColor.a = Mathf.Lerp(a, 0f, elapsed / fadeDuration);
            hurtImage.color = tempColor;
            yield return null;
        }

        hurtImage.enabled = false;
    }
}
