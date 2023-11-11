using Game.GameSystem;
using Game.Item;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerAnimatorController))]
    public class PlayerAim : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] PlayerAnimatorController m_playerAnimatorController;
        [SerializeField] LayerMask m_enemyLayerMask;

        [Header("Gun Settings")]
        [SerializeField] AudioClip m_gunClip;
        [SerializeField] Transform m_firePoint;
        [SerializeField, Min(0)] float m_pistolDistance;
        [SerializeField, Range(0f, 1)] float m_fireRate;

        [SerializeField] UnityEvent m_aimingEvent;
        [SerializeField] UnityEvent m_notAimingEvent;

        [Header("Ammo Settings")]
        [SerializeField, Min(0)] int m_maxAmmo;
        int m_currentAmmo;

        float m_fireRateTimer;
        bool m_aiming;
        public bool m_Aiming => m_aiming;

        private void Start()
        {
            m_currentAmmo = m_maxAmmo;
            m_aiming = false;
            m_fireRateTimer = Mathf.Abs(m_fireRate - 1);
        }

        private void Update()
        {
            m_fireRateTimer -= Time.deltaTime;
            if (GameManager.Instance.m_IsPaused)
            {
                m_aiming = false;
            }
            if (m_aiming)
            {
                m_aimingEvent?.Invoke();
            }
            else
            {
                m_notAimingEvent?.Invoke();
            }
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
            AudioSource.PlayClipAtPoint(m_gunClip, m_firePoint.position);
            m_playerAnimatorController.ShootAnim();
            m_fireRateTimer = Mathf.Abs(m_fireRate - 1);
            m_currentAmmo -= 1;
            ShootHit();
            bool CanShoot()
            {
                if (!m_aiming) return false;
                if (m_currentAmmo <= 0) return false;
                return m_fireRateTimer <= 0;
            }
        }

        public void Reload()
        {
            if (m_currentAmmo >= m_maxAmmo) return;
            m_currentAmmo = m_maxAmmo;
        }
        void OnDrawGizmos()
        {
            if (m_firePoint is null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(m_firePoint.position, m_firePoint.forward * m_pistolDistance);
        }

        void ShootHit()
        {
            bool hit = Physics.Raycast(m_firePoint.position, m_firePoint.forward, out RaycastHit raycastHit, m_pistolDistance, m_enemyLayerMask);
            if (hit)
            {
                raycastHit.transform.GetComponent<LifeSystem>().Damage(1);
            }
        }
    }  
}