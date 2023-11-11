using MigalhaSystem.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateMachine : AbstractStateMachineController
{
    [Header("States")]
    [SerializeField] StateZombieChasing m_zombieChasingState;
    [SerializeField] StateZombieAttack m_zombieAttackState;
    [SerializeField] StateZombieDie m_zombieDieState;

    [Header("Raycast Settings")]
    [SerializeField] Vector3 m_raycastOffset;
    [SerializeField] float m_raycastDistance;
    [SerializeField] LayerMask m_playerLayerMask;

    public StateZombieChasing m_ZombieChasingState => m_zombieChasingState;
    public StateZombieAttack m_ZombieAttackState => m_zombieAttackState;
    public StateZombieDie m_ZombieDieState => m_zombieDieState;
    private void Start()
    {
        ForceSwitchState(m_zombieChasingState);
    }

    public bool PlayerInFront()
    {
        return Physics.Raycast(transform.position + m_raycastOffset, transform.forward, m_raycastDistance, m_playerLayerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position + m_raycastOffset, transform.forward * m_raycastDistance);
        m_zombieAttackState.DrawCol(transform);
    }
}
