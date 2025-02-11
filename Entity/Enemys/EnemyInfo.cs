using System;
using UnityEngine;

public class EnemyInfo : Entity 
{
    public override void Die()
    {
        GameObject.Find("Player").GetComponent<PlayerInfo>().Health+=100;
        Destroy(gameObject);
    }
    void Start()
    {
        Health = MaxHealth;
    }
}
