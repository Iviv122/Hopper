using System;
using System.Net.Mail;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity 
{
    public delegate void OnDisable(Enemy Instance);
    public OnDisable Disable;

    public AttackRadius attackRadius;
    public EnemyMovement Movement;
    public NavMeshAgent Agent;
    public EnemyScriptableObject EnemyScriptableObject;
    public event Action Death;
    public virtual void OnEnable() {
       SetupAgentFromConfiguration(); 
    } 
    public override void Die()
    {
       Disable(this);
       Agent.enabled = false; 
    }
    public virtual void SetupAgentFromConfiguration()
    {
        Agent.acceleration = EnemyScriptableObject.Acceleration;
        Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;
        Agent.areaMask = EnemyScriptableObject.AreaMask;
        Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
        Agent.baseOffset = EnemyScriptableObject.BaseOffset;
        Agent.height = EnemyScriptableObject.Height;
        Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
        Agent.radius = EnemyScriptableObject.Radius;
        Agent.speed = EnemyScriptableObject.Speed;
        Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;
        
        Movement.UpdateSpeed = EnemyScriptableObject.AIUpdateInterval;

        Health = EnemyScriptableObject.Health;

        attackRadius.Collider.radius = EnemyScriptableObject.attackRadius;
        attackRadius.AttackDelay = EnemyScriptableObject.AttackDelay;
        attackRadius.Damage = EnemyScriptableObject.Damage;
    }
}
