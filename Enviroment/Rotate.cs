using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;

    private void Update() {

    transform.Rotate(x,y,z);

    }
}
