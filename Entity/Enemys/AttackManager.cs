using System;
using UnityEngine;

abstract public class AttackManager : MonoBehaviour
{
    virtual public void AttackRange(Transform target){
        throw new NotImplementedException();
    }
    virtual public void AttackMelee(Transform target){
        throw new NotImplementedException();
    }
}
