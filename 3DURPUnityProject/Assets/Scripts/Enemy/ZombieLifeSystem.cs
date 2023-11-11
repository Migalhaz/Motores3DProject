using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLifeSystem : LifeSystem
{
    [SerializeField] CapsuleCollider m_collider;
    [SerializeField] ZombieStateMachine m_zombieStateMachine;
    [SerializeField] AudioClip m_zombieSpawn;
    [SerializeField] AudioClip m_damageClip;
    [SerializeField] AudioClip m_deathClip;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(m_zombieSpawn, transform.position);
    }

    public override void Damage(float _damageValue)
    {
        AudioSource.PlayClipAtPoint(m_damageClip, transform.position);
        base.Damage(_damageValue);
    }

    protected override void Death()
    {
        m_collider.enabled = false;
        AudioSource.PlayClipAtPoint(m_deathClip, transform.position);
        PlayerManager.Instance.ZombieKilled();
        m_zombieStateMachine.ForceSwitchState(m_zombieStateMachine.m_ZombieDieState);
    }
}
