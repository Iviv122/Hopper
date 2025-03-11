using UnityEngine;

public class GeneralDestructable : Entity 
{
    public override void Die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        Health = MaxHealth;
    }
    
}
