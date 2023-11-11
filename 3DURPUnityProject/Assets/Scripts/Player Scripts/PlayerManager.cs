using MigalhaSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField] PlayerInputs m_playerInputs;
        [SerializeField] PlayerAnimatorController m_playerAnimatorController;
        [SerializeField] PlayerAim m_playerAim;
        [SerializeField] PlayerLifeSystem m_playerLifeSystem;
        [SerializeField] PlayerMove m_playerMove;
        float m_coins;
        [SerializeField] TMPro.TextMeshProUGUI m_textMeshPro;
        public PlayerInputs m_PlayerInputs 
        {
            get
            {
                if (m_playerInputs is null)
                {
                    m_playerInputs = GetComponent<PlayerInputs>();
                }
                return m_playerInputs;
            }               
        }
        public PlayerAnimatorController m_PlayerAnimatorController
        {
            get
            {
                if (m_playerAnimatorController is null)
                {
                    m_playerAnimatorController = GetComponent<PlayerAnimatorController>();
                }
                return m_playerAnimatorController;
            }
        }
        public PlayerAim m_PlayerAim
        {
            get
            {
                if (m_playerAim is null)
                {
                    m_playerAim = GetComponent<PlayerAim>();
                }
                return m_playerAim;
            }
        }
        public PlayerLifeSystem m_PlayerLifeSystem
        {
            get
            {
                if (m_playerLifeSystem is null)
                {
                    m_playerLifeSystem = GetComponent<PlayerLifeSystem>();
                }
                return m_playerLifeSystem;
            }
        }
        public PlayerMove m_PlayerMove
        {
            get
            {
                if (m_playerMove is null)
                {
                    m_playerMove = GetComponent<PlayerMove>();
                }
                return m_playerMove;
            }
        }

        private void Start()
        {
            m_coins = 0;
        }

        private void Update()
        {
            m_textMeshPro.text = $"x{m_coins}";
        }

        public void AddCoin()
        {
            m_coins += 1;
        }

    }
}