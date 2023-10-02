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

        [Header("Aim Settings")]
        [SerializeField] UnityEvent m_mouseRightButtonPressed;
        [SerializeField] UnityEvent m_mouseRightButtonReleased;
        [SerializeField] UnityEvent m_mouseLeftButtonPressed;

        private void Update()
        {
            MoveInput();
            AimInput();
        }

        void MoveInput()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            m_moveInputs.Set(x, y);
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