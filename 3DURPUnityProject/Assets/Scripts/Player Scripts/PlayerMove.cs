using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerInputs))]
    [RequireComponent(typeof(CharacterController))]
    [DisallowMultipleComponent()]
    public class PlayerMove : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] CharacterController m_characterController;
        [SerializeField] PlayerInputs m_inputs;

        [Header("Physics")]
        [SerializeField, Min(0)] float m_gravityScale = 1f;

        [Header("Move Settings")]
        [SerializeField, Min(0)] float m_moveSpeed = 3f;
        Vector3 m_moveDirection;

        private void Update()
        {
            Move();
        }

        void Move()
        {
            m_moveDirection.Set(m_inputs.m_MoveInputs.x, m_gravityScale * -1f, m_inputs.m_MoveInputs.y);
            Vector3 moveResult = transform.TransformDirection(m_moveDirection).normalized;
            float moveSpeedResult = m_moveSpeed * Time.deltaTime;
            m_characterController.Move(moveSpeedResult * moveResult);
        }
    }
}