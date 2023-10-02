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
        [SerializeField] PlayerInventory m_playerInventory;
        [SerializeField] PlayerMove m_playerMove;

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
        public PlayerInventory m_PlayerInventory
        {
            get
            {
                if (m_playerInventory is null)
                {
                    m_playerInventory = GetComponent<PlayerInventory>();
                }
                return m_playerInventory;
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

    }
}