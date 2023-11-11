using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : LifeSystem
{
    [SerializeField] UnityEngine.UI.Image m_playerLife;
    public bool m_alive { get { return m_currentHealth > 0; } }
    
    private void Update()
    {
        m_playerLife.fillAmount = m_currentHealth / m_startHealth;
    }
}
