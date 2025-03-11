using UnityEngine;

public class BlowAble : CanDestroy 
{
    void DestroyYourself(){
        Destroy(gameObject);
    }
}
