using UnityEngine;

public class CanDestroy : MonoBehaviour
{
    [SerializeField] int HealthBeforeDestruction;
    
    public int Health{
        get{return HealthBeforeDestruction;}
        set{
            HealthBeforeDestruction = value;
            if(HealthBeforeDestruction <= 0){
                DestroyYourself();
            }
        }
    }
    void DestroyYourself(){
        Destroy(gameObject);
    }
}
