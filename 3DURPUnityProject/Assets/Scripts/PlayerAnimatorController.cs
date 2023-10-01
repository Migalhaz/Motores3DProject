using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerInputs))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] PlayerAim m_playerAim;
        [SerializeField] PlayerInputs m_playerInputs;

        [Header("Player Animator Settings")]
        [SerializeField] Animator m_playerAnimatorController;
        int m_xInputAnimatorHash = Animator.StringToHash("xInput");
        int m_yInputAnimatorHash = Animator.StringToHash("yInput");

        [Header("Gun Animator Settings")]
        [SerializeField] Animator m_gunAnimatorController;
        int m_AimingBoolHash = Animator.StringToHash("Aim");
        int m_ShootTriggerHash = Animator.StringToHash("Shoot");
        void Update()
        {
            SetPlayerInputAnimatorValues();
            SetAimAnimatorValue();
        }

        void SetPlayerInputAnimatorValues()
        {
            m_playerAnimatorController.SetFloat(m_xInputAnimatorHash, m_playerInputs.m_MoveInputs.x);
            m_playerAnimatorController.SetFloat(m_yInputAnimatorHash, m_playerInputs.m_MoveInputs.y);
        }

        void SetAimAnimatorValue()
        {
            m_gunAnimatorController.SetBool(m_AimingBoolHash, m_playerAim.m_Aiming);
        }
        public void ShootAnim()
        {
            m_gunAnimatorController.SetTrigger(m_ShootTriggerHash);
        }
    }
}