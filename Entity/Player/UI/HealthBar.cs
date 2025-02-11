using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerInfo player;
    [SerializeField] TextMeshProUGUI textField;
    private void Awake() {
        player.HealthChanged += UpdateText;
        UpdateText();
    }
    private void UpdateText(){
        textField.text = player.Health + "/" + player.MaxHealth;
    }

}
