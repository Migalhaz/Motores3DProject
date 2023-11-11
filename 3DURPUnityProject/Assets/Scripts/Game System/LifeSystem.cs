using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeSystem : MonoBehaviour
{
    [SerializeField] protected float m_startHealth;
    protected float m_currentHealth;

    private void Awake()
    {
        m_currentHealth = m_startHealth;
    }

    public virtual void Damage(float _damageValue)
    {
        m_currentHealth -= _damageValue;
        if (m_currentHealth <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {

    }
}
