using System;
using UnityEngine;

abstract public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;
    public event Action HealthChanged;
    public int MaxHealth{
        get{return maxHealth;}
    }
    public int Health{
        get{return health;}
        set{

            health = value;
            if(health <= 0){
                health = 0;
                Die(); 
            }
            if(health > MaxHealth){
                health = MaxHealth;
            }

            HealthChanged?.Invoke();
        }
    }
    public void InvokeHealthChanged(){
        HealthChanged?.Invoke();
    }
    public virtual void GetDamage(int Damage)
    {
        Health -= Damage;
        HealthChanged?.Invoke(); 
    }
    abstract public void Die();
}
