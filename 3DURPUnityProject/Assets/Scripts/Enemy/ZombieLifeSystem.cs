using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLifeSystem : LifeSystem
{
    [SerializeField] ZombieStateMachine m_zombieStateMachine;
    protected override void Death()
    {
        m_zombieStateMachine.ForceSwitchState(m_zombieStateMachine.m_ZombieDieState);
    }
}
