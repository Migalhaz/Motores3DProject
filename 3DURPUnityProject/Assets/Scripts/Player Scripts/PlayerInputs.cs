using Game.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    [DisallowMultipleComponent()]
    public class PlayerInputs : MonoBehaviour
    {
        [Header("Player Move Settings")]
        Vector2 m_moveInputs;
        public Vector2 m_MoveInputs => m_moveInputs;

        [Header("Interact Settings")]
        [SerializeField] KeyCode m_interactKeyCode = KeyCode.E;
        public UnityEvent m_InteractEvent;

        [Header("Aim Settings")]
        [SerializeField] UnityEvent m_mouseRightButtonPressed;
        [SerializeField] UnityEvent m_mouseRightButtonReleased;
        [SerializeField] UnityEvent m_mouseLeftButtonPressed;

        [Header("Reload Settings")]
        [SerializeField] KeyCode m_reloadKeycode = KeyCode.R;
        [SerializeField] UnityEvent m_reloadEvent;

        private void Update()
        {
            if (GameManager.Instance.m_IsPaused) return;
            if (!PlayerManager.Instance.m_PlayerLifeSystem.m_alive) return;
            MoveInput();
            AimInput();
            ReloadInput();
            InteractInput();
        }

        void MoveInput()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            m_moveInputs.Set(x, y);
        }

        void ReloadInput()
        {
            if (Input.GetKeyDown(m_reloadKeycode))
            {
                m_reloadEvent?.Invoke();
            }
        }

        void InteractInput()
        {
            if (Input.GetKeyDown(m_interactKeyCode))
            {
                m_InteractEvent?.Invoke();
            }
        }

        void AimInput()
        {
            Aim();
            Shoot();
            void Aim()
            {
                if (Input.GetMouseButtonDown(1))
                {
                    m_mouseRightButtonPressed?.Invoke();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    m_mouseRightButtonReleased?.Invoke();
                }
            }
            void Shoot()
            {
                if (Input.GetMouseButton(0))
                {
                    m_mouseLeftButtonPressed?.Invoke();
                }
            }
        }
    }
}