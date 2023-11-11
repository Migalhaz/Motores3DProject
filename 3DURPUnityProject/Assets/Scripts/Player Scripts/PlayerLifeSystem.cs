using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : LifeSystem
{
    [SerializeField] UnityEngine.UI.Image m_playerLife;
    [SerializeField] AudioClip m_damageSound;
    public bool m_alive { get { return m_currentHealth > 0; } }
    
    private void Update()
    {
        m_playerLife.fillAmount = m_currentHealth / m_startHealth;
    }

    public override void Damage(float _damageValue)
    {
        AudioSource.PlayClipAtPoint(m_damageSound, transform.position);
        base.Damage(_damageValue);
    }
}
