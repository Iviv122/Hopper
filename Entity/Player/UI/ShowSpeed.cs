using TMPro;
using UnityEngine;

public class ShowSpeed : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public TextMeshProUGUI textfield;

    void Update(){
        textfield.text = (rb.linearVelocity.magnitude).ToString();
    }
}
