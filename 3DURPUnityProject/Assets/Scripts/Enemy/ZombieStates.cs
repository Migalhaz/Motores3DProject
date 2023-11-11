using Game.Player;
using MigalhaSystem.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class StateZombieChasing : AbstractState
{
    [Header("Components")]
    [SerializeField] NavMeshAgent m_navmeshAgent;
    Transform m_playerTransform;

    [Header("Animation Settings")]
    [SerializeField] Animator m_animator;
    [SerializeField] string m_animationTag;
    [SerializeField, Min(0)] float m_transitionTime;

    public override void EnterState(AbstractStateMachineController stateMachineController)
    {
        base.EnterState(stateMachineController);

        m_animator.CrossFade(m_animationTag, m_transitionTime);
        m_playerTransform = PlayerManager.Instance.transform;
        m_navmeshAgent.isStopped = false;
    }

    public override void UpdateState(AbstractStateMachineController stateMachineController)
    {
        base.UpdateState(stateMachineController);
        m_navmeshAgent.SetDestination(m_playerTransform.position);
        ZombieStateMachine zombieStateMachine = (ZombieStateMachine)stateMachineController;

        if (!zombieStateMachine.PlayerInFront()) return;

        zombieStateMachine.ForceSwitchState(zombieStateMachine.m_ZombieAttackState);
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
