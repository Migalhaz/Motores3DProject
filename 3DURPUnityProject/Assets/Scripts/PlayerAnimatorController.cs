using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerInputs))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] Animator m_animatorController;
        [SerializeField] PlayerInputs m_playerInputs;
        int m_xInputAnimatorHash = Animator.StringToHash("xInput");
        int m_yInputAnimatorHash = Animator.StringToHash("yInput");

        void Update()
        {
            SetInputAnimatorValues();
        }

        void SetInputAnimatorValues()
        {
            m_animatorController.SetFloat(m_xInputAnimatorHash, m_playerInputs.m_MoveInputs.x);
            m_animatorController.SetFloat(m_yInputAnimatorHash, m_playerInputs.m_MoveInputs.y);
        }
    }
}