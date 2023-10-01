using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerAnimatorController))]
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] PlayerAnimatorController m_playerAnimatorController;
        [SerializeField, Range(0f, 1)] float m_fireRate;
        float m_fireRateTimer;
        bool m_aiming;
        public bool m_Aiming => m_aiming;

        private void Start()
        {
            m_aiming = false;
            m_fireRateTimer = Mathf.Abs(m_fireRate - 1);
        }

        private void Update()
        {
            m_fireRateTimer -= Time.deltaTime;
        }
        public void Aiming()
        {
            m_aiming = true;
        }

        public void Idle()
        {
            m_aiming = false;
        }

        public void Shoot()
        {
            if (!CanShoot()) return;
            m_playerAnimatorController.ShootAnim();
            m_fireRateTimer = Mathf.Abs(m_fireRate - 1);
            bool CanShoot()
            {
                if (!m_aiming) return false;
                return m_fireRateTimer <= 0;
            }
        }


    }
}