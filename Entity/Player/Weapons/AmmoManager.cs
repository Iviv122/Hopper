using System;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] int nails;
    [SerializeField] int rockets;
    [SerializeField] int buckshots;

    public event Action AmmoChanged; 
    public int Nails{
        get{return nails;}
        set{

            nails = value;
            if(nails <= 0){
                nails = 0;
            }
            AmmoChanged?.Invoke();
        }
    }
    public int Rockets{
        get{return rockets;}
        set{

            rockets = value;
            if(rockets <= 0){
                rockets = 0;
            }
            AmmoChanged?.Invoke();
        }
    }
    public int BuckShots{
        get{return buckshots;}
        set{

            buckshots = value;
            if(buckshots <= 0){
                buckshots = 0;
            }
            AmmoChanged?.Invoke();
        }
    }
    public int CheckAmmoAmount(AmmoType type){
        switch (type)
        {
            case AmmoType.Nails:
                return nails;
            case AmmoType.Rocket:
                return rockets;
            case AmmoType.BuckShot:
                return buckshots;
            default:
                return nails;
        }
    }
    public void RemoveAmmo(AmmoType type, int amount){
        switch (type)
        {
            case AmmoType.Nails:
                Nails-=amount;
            break;
            case AmmoType.Rocket:
                Rockets-=amount;
            break;
            case AmmoType.BuckShot:
                BuckShots-=amount;
            break;
        }
    }
}
public enum AmmoType{
    Nails,
    Rocket,
    BuckShot,
    // other
}