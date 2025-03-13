using System;
using UnityEngine;

abstract public class Enemy : Entity 
{
    public delegate void OnDisable(Enemy Instance);
    public OnDisable Disable;
    [Header("Vital Systems")]
    public AttackManager attackManager;
    public EnemyMovement movement;
    public EnemyScriptableObject EnemyScriptableObject;
    public WaitForSeconds UpdateSpeed;
    public bool DestroyOnDie = false;
    [Header("Var")]
    public Transform Target;


    public event Action Death;
    public virtual void OnEnable() {
       SetupAgentFromConfiguration();
    }
    public void Setup(Transform _Target){
        Target = _Target;
        movement.Target = Target;
        movement.Agent.enabled = true; 
    }
    public bool InRadius(Transform target, float Radius){
        return (gameObject.transform.position - target.position).magnitude < Radius; 
    }
    public bool HasLineOfSightTo(Transform _Target, float Radius, float BulletRadius, LayerMask masks){
        if (Physics.SphereCast(transform.position, BulletRadius, (_Target.position - transform.position).normalized, out RaycastHit Hit, Radius, masks))
        {
            Entity prey;
            if (prey = Hit.collider.GetComponent<Entity>())
            {
                return prey.gameObject.transform == _Target;
            }
            if (prey = Hit.collider.GetComponentInParent<Entity>())
            {
                return prey.gameObject.transform == _Target;
            }
        }

        return false; 
    }
    public override void Die()
    {
        if(DestroyOnDie){
            Destroy(gameObject);
        }
        else{
            Disable(this);
            movement.Agent.enabled = false; 
        }
    }
    public virtual void SetupAgentFromConfiguration()
    {
        //Setup Movement Variables
        movement.Agent.acceleration = EnemyScriptableObject.Acceleration;
        movement.Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;
        movement.Agent.areaMask = EnemyScriptableObject.AreaMask;
        movement.Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
        movement.Agent.baseOffset = EnemyScriptableObject.BaseOffset;
        movement.Agent.height = EnemyScriptableObject.Height;
        movement.Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
        movement.Agent.radius = EnemyScriptableObject.Radius;
        movement.Agent.speed = EnemyScriptableObject.Speed;
        movement.Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;

        movement.Target = Target;

        UpdateSpeed = new WaitForSeconds(EnemyScriptableObject.AIUpdateInterval);

        maxHealth = EnemyScriptableObject.Health;
        Health = MaxHealth; 
    }
}
