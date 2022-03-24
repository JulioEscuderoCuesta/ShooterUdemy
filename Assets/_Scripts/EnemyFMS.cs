using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Sight))]
public class EnemyFMS : MonoBehaviour
{
    private const float Tolerance = 1.1F;
    public enum EnemyState {GoToBase, ChasePlayer, AttackPlayer, AttackBase}

    public EnemyState currentState;

    private Sight _sight;

    public Transform baseTransform;

    public float baseAttackDistance, playerAttackDistance;

    private NavMeshAgent _agent;
    private Animator _animator;

    [SerializeField]
    [Tooltip("Arma")]
    private Weapon weapon;
    


    private void Awake()
    {
        _sight = GetComponent<Sight>();
        baseTransform = GameObject.FindWithTag("Base").transform;
        _agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.GoToBase:
                GoToBase();
                break;
            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;
            case EnemyState.AttackPlayer:
                AttackPlayer();
                break;
            case EnemyState.AttackBase:
                AttackBase();
                break;
            default:
                GoToBase();
                break;
        }
    }

    private void GoToBase()
    {
        //print("ir a base");
        _animator.SetBool("Shoot Bullet Bool", false);
        _agent.isStopped = false;
        _agent.SetDestination(baseTransform.position);
        if (_sight.detectedTarget != null)
        {
            currentState = EnemyState.ChasePlayer;
            return;
        }
        
        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
        if (distanceToBase < baseAttackDistance)
            currentState = EnemyState.AttackBase;
    }

    private void ChasePlayer()
    {
        //print("perseguir jugador");
        _animator.SetBool("Shoot Bullet Bool", false);
        if (_sight.detectedTarget == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        _agent.isStopped = false;
        _agent.SetDestination(_sight.detectedTarget.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, _sight.detectedTarget.transform.position);
        if (distanceToPlayer < playerAttackDistance)
            currentState = EnemyState.AttackPlayer;
    }

    private void AttackPlayer()
    {
       // print("atacar jugador");
        _agent.isStopped = true;
        if (_sight.detectedTarget == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookAt(_sight.detectedTarget.transform.position);
        ShootTarget();

        float distanceToPlayer = Vector3.Distance(transform.position, _sight.detectedTarget.transform.position);
        if (distanceToPlayer > playerAttackDistance * Tolerance)
            currentState = EnemyState.ChasePlayer;
        

    }

    private void AttackBase()
    {
        //print("atacar base");
        _agent.isStopped = true;
        LookAt(baseTransform.position);
        ShootTarget();
    }


    private void ShootTarget()
    {
        if(weapon.ShootBullet("Enemy Bullet", 0))
            _animator.SetBool("Shoot Bullet Bool", true);
    }

    private void LookAt(Vector3 targetPos)
    {
        var directionLook = Vector3.Normalize(targetPos - transform.position);
        directionLook.y = 0;
        transform.parent.forward = directionLook;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }
}
