using Game.Item;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerAnimatorController))]
    [RequireComponent(typeof(PlayerInventory))]
    public class PlayerAim : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] PlayerAnimatorController m_playerAnimatorController;
        [SerializeField] PlayerInventory m_inventory;

        [Header("Gun Settings")]
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
            m_playerAnimatorController.ShootAnim();
            m_fireRateTimer = Mathf.Abs(m_fireRate - 1);
            m_currentAmmo -= 1;
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
            List<AbstractItem> itens = m_inventory.m_CurrentItens;
            List<AbstractItem>  ammoBoxes = itens.FindAll(x => x is AmmoBox);
            if (ammoBoxes.Count <= 0) return;

            AmmoBox currentAmmoBox = (AmmoBox)ammoBoxes.First();
            m_currentAmmo += currentAmmoBox.m_AmmoValue;
            if (m_currentAmmo >= m_maxAmmo)
            {
                m_currentAmmo = m_maxAmmo;
            }
            m_inventory.RemoveItem(currentAmmoBox);
        }
    }
}