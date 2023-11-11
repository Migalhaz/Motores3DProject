using Game.GameSystem;
using Game.Player;
using MigalhaSystem.StateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class StateZombiePatrol : AbstractState
{
    [SerializeField] Animator m_animator;
    [SerializeField] string m_idleTag;
    [SerializeField] string m_walkTag;
    [SerializeField] float m_transitionDuration = .1f;


    [SerializeField] NavMeshAgent m_navmesh;
    List<Transform> m_waypoints;
    Transform m_currentWaypoint;

    [SerializeField] float m_timerToWait;
    float m_currentTimer;

    [SerializeField] float m_distanceToChasePlayer = 50;

    public override void EnterState(AbstractStateMachineController stateMachineController)
    {
        base.EnterState(stateMachineController);
        m_waypoints = GameManager.Instance.m_waypoints;
        SetWaypoint();
        m_currentTimer = 0;
        m_animator.CrossFade(m_walkTag, m_transitionDuration);
    }

    public override void UpdateState(AbstractStateMachineController stateMachineController)
    {
        base.UpdateState(stateMachineController);

        ZombieStateMachine zombieStateMachine = (ZombieStateMachine)stateMachineController;

        Vector3 waypointPos = m_currentWaypoint.position;
        waypointPos.y = 0;

        Vector3 zombiePos = stateMachineController.transform.position;
        zombiePos.y = 0;

        Vector3 playerPos = PlayerManager.Instance.transform.position;
        playerPos.y = 0;

        float distanceFromWaypoint = Vector3.Distance(waypointPos, zombiePos);
        if (distanceFromWaypoint <= 3)
        {
            Debug.Log("wait timer");
            m_navmesh.isStopped = true;
            m_animator.CrossFade(m_idleTag, m_transitionDuration);
            m_currentTimer += Time.deltaTime;

            if (m_currentTimer >= m_timerToWait)
            {
                Debug.Log("picking waypoint");
                SetWaypoint();
                m_currentTimer = 0;
                m_animator.CrossFade(m_walkTag, m_transitionDuration);
            }
            
        }

        float distanceFromPlayer = Vector3.Distance(zombiePos, playerPos);
        if (distanceFromPlayer <= m_distanceToChasePlayer)
        {
            zombieStateMachine.SwitchState(zombieStateMachine.m_ZombieChasingState);
        }
    }

    public void SetWaypoint()
    {
        int waypoint = Random.Range(0, m_waypoints.Count);
        m_currentWaypoint = m_waypoints[waypoint];
        m_navmesh.isStopped = false;
        m_navmesh.SetDestination(m_currentWaypoint.position);
    }
}

[System.Serializable]
public class StateZombieChasing : AbstractState
{
    [Header("Audio")]
    [SerializeField] AudioClip m_zombieSound;
    [SerializeField] float m_timerToSound;
    float m_currentTimer;

    [Header("Components")]
    [SerializeField] NavMeshAgent m_navmeshAgent;
    Transform m_playerTransform;

    [SerializeField] float m_distanceToChase;

    [Header("Animation Settings")]
    [SerializeField] Animator m_animator;
    [SerializeField] string m_animationTag;
    [SerializeField, Min(0)] float m_transitionTime;

    public override void EnterState(AbstractStateMachineController stateMachineController)
    {
        base.EnterState(stateMachineController);
        m_currentTimer = 0;
        m_animator.CrossFade(m_animationTag, m_transitionTime);
        m_playerTransform = PlayerManager.Instance.transform;
        m_navmeshAgent.isStopped = false;
    }

    public override void UpdateState(AbstractStateMachineController stateMachineController)
    {
        base.UpdateState(stateMachineController);
        m_navmeshAgent.SetDestination(m_playerTransform.position);
        ZombieStateMachine zombieStateMachine = (ZombieStateMachine)stateMachineController;
        m_currentTimer += Time.deltaTime;
        if (m_currentTimer >= m_timerToSound)
        {
            AudioSource.PlayClipAtPoint(m_zombieSound, zombieStateMachine.transform.position);
            m_currentTimer = 0;
        }

        Vector3 zombiePos = stateMachineController.transform.position;
        zombiePos.y = 0;

        Vector3 playerPos = PlayerManager.Instance.transform.position;
        playerPos.y = 0;

        float distanceFromPlayer = Vector3.Distance(zombiePos, playerPos);
        if (distanceFromPlayer > m_distanceToChase)
        {
            zombieStateMachine.SwitchState(zombieStateMachine.m_ZombiePatrolState);
        }

        if (!zombieStateMachine.PlayerInFront()) return;

        zombieStateMachine.SwitchState(zombieStateMachine.m_ZombieAttackState);
    }

    public override void ExitState(AbstractStateMachineController stateMachineController)
    {
        base.ExitState(stateMachineController);
        m_navmeshAgent.isStopped = true;
    }
}

[System.Serializable]
public class StateZombieAttack : AbstractState
{
    [Header("Animation Settings")]
    [SerializeField] Animator m_animator;
    [SerializeField] string m_animationTag;
    [SerializeField, Min(0)] float m_transitionTime;

    [Header("State Settings")]
    [SerializeField] AudioClip m_attackSound;
    [SerializeField, Min(0)] float m_timerExitAttack;
    [SerializeField, Min(0)] float m_timerExecAttack;
    float m_currentTime;
    bool m_attacked;

    [Header("Collider Settings")]
    [SerializeField] Vector3 m_offset;
    [SerializeField, Min(0)] float m_size;
    [SerializeField] LayerMask m_playerLayerMask;

    public override void EnterState(AbstractStateMachineController stateMachineController)
    {
        base.EnterState(stateMachineController);
        AudioSource.PlayClipAtPoint(m_attackSound, stateMachineController.transform.position);
        m_animator.CrossFade(m_animationTag, m_transitionTime);
        m_currentTime = 0;
        m_attacked = false;
    }

    public override void UpdateState(AbstractStateMachineController stateMachineController)
    {
        base.UpdateState(stateMachineController);
        ZombieStateMachine zombieStateMachine = (ZombieStateMachine) stateMachineController;

        m_currentTime += Time.deltaTime;
        if (m_currentTime >= m_timerExecAttack)
        {
            if (!m_attacked)
            {
                Attack(stateMachineController.transform);
            }
        }

        if (m_currentTime >= m_timerExitAttack)
        {
            zombieStateMachine.ForceSwitchState(zombieStateMachine.m_ZombieChasingState);
        }
    }

    void Attack(Transform zombieTransform)
    {
        m_attacked = true;

        bool colCheck = Physics.CheckSphere(zombieTransform.TransformPoint(m_offset), m_size, m_playerLayerMask);
        if (colCheck)
        {
            PlayerManager.Instance.m_PlayerLifeSystem.Damage(1);
        }
    }

    public void DrawCol(Transform zombieTransform)
    {
        Gizmos.DrawWireSphere(zombieTransform.TransformPoint(m_offset), m_size);
    }
}

[System.Serializable]
public class StateZombieDie : AbstractState
{
    [Header("Animation Settings")]
    [SerializeField] Animator m_animator;
    [SerializeField] string m_animationTag;
    [SerializeField, Min(0)] float m_transitionTime;
    [SerializeField] NavMeshAgent m_navMeshAgent;
    [SerializeField, Min(0)] float m_timeToDestroyObject;
    float m_currentTime;
    public override void EnterState(AbstractStateMachineController stateMachineController)
    {
        if (m_navMeshAgent.enabled)
        {
            m_navMeshAgent.isStopped = true;
            m_navMeshAgent.enabled = false;
        }
        
        base.EnterState(stateMachineController);
        m_animator.CrossFade(m_animationTag, m_transitionTime);
    }

    public override void UpdateState(AbstractStateMachineController stateMachineController)
    {
        base.UpdateState(stateMachineController);
        m_currentTime += Time.deltaTime;

        if (m_currentTime >= m_timeToDestroyObject)
        {
            Object.Destroy(stateMachineController.gameObject);
        }
    }
}
