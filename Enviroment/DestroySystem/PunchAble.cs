using UnityEngine;

public class PunchAble : CanDestroy 
{
    void DestroyYourself(){
        Destroy(gameObject);
    }
}
