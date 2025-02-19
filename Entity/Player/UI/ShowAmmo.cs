using TMPro;
using UnityEngine;

public class ShowAmmo : MonoBehaviour
{
    [SerializeField] AmmoManager ammo;
    [SerializeField] TextMeshProUGUI nails;
    [SerializeField] TextMeshProUGUI rockets;
    [SerializeField] TextMeshProUGUI buckshots;
    void Awake(){
        ammo.AmmoChanged += UpdateAmmo;
        UpdateAmmo(); 
    }
    void UpdateAmmo(){
        nails.text = ammo.Nails.ToString();
        rockets.text = ammo.Rockets.ToString();
        buckshots.text = ammo.BuckShots.ToString();
    }
}
