using System;
using System.Collections;
using UnityEngine;

public class PlayerInfo : Entity 
{
    [SerializeField] public float IntervalBetweenDamage;
    [SerializeField] public int DamageInInterval;
    [SerializeField] public bool isInvincible;
    [SerializeField] private int armour;
    public event Action ArmourChanged;
    public int Armour{
        get{return armour;}
        set{
            armour = value;
            if(armour <= 0){
                armour = 0;
            }
            if(armour > 200){ //max armour
                armour = 200;
            }
            ArmourChanged?.Invoke();
        }
    } 
    public void Start(){
        Health = MaxHealth;
        InvokeRepeating(nameof(ApplySelfDamage),0,IntervalBetweenDamage); 
    }
    public override void GetDamage(int damage){
        
        if(!isInvincible){
            if(armour >0){
                int damageRem = damage/2-Armour;
                Armour -= damage/2;
                if(damageRem > 0){
                    damage -= damageRem;
                }
            }
            Health -= damage;
            StartCoroutine(IFrames());
            InvokeHealthChanged(); 
        }
    }
    public void GetDamageNoIFrames(int damage){
        
            if(armour >0){
                int damageRem = damage/2-Armour;
                Armour -= damage/2;
                if(damageRem > 0){
                    damage -= damageRem;
                }
            }
            Health -= damage;
    }
    public void ApplySelfDamage(){
            if(Health > 100){
            Health -= DamageInInterval;
            }
            Health -= DamageInInterval;
            InvokeHealthChanged();
    }
    IEnumerator IFrames(){
        isInvincible = true;
        yield return new WaitForSeconds(0.1f);
        isInvincible = false;
    }
    public override void Die()
    {
        Debug.Log("Now i am dead, but i will return >:3");
    }
}
