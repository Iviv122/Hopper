using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthCanvas : MonoBehaviour
{
    [SerializeField] PlayerInfo player;
    [SerializeField] private Image redSplatterImage = null;
    [SerializeField] private Image hurtImage;
    [SerializeField] private float hurtTimer = 0.1f;
    void Awake()
    {
        player.HealthChanged+=UpdateHealth;
        player.DamageTaken += OnDamageTaken;
    }

    void UpdateHealth(){
        if(player.Health <= 0) return;
        Color splatterAlpha = new Color(1,0,0,1-((float)player.Health/player.MaxHealth*2));
        //Debug.Log(splatterAlpha);
        redSplatterImage.color = splatterAlpha;
    }
    
    private void OnDamageTaken()
    {
        StartCoroutine(HurtFlash());
    }
    IEnumerator HurtFlash(){
        hurtImage.enabled = true;
        yield return new WaitForSeconds(hurtTimer);
        hurtImage.enabled = false;

    }
}
