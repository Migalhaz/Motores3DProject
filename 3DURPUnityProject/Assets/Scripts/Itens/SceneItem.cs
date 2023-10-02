using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item
{
    public class SceneItem : MonoBehaviour
    {
        [SerializeField] AbstractItem m_currentItem;
        [SerializeField] Vector3 m_colliderOffset;
        [SerializeField, Min(0)] float m_colliderRadius = 1f;
        [SerializeField] LayerMask m_playerLayerMask;
        Player.PlayerInputs m_playerInputs;
        Player.PlayerInventory m_playerInventory;

        bool eventAdd;

        void Start()
        {
            Player.PlayerManager playerManager = Player.PlayerManager.Instance;
            eventAdd = false;
            m_playerInputs = playerManager.m_PlayerInputs;
            m_playerInventory = playerManager.m_PlayerInventory;
        }

        private void Update()
        {
            SetInteractEvent();
        }

        public void GetItem()
        {
            bool add = m_playerInventory.AddItem(m_currentItem);
            if (add)
            {
                Destroy(gameObject);
            }
            
        }

        void SetInteractEvent()
        {
            if (InArea())
            {
                if (!eventAdd)
                {
                    eventAdd = true;
                    m_playerInputs.m_InteractEvent.AddListener(GetItem);
                }
            }
            else
            {
                if (eventAdd)
                {
                    eventAdd = false;
                    m_playerInputs.m_InteractEvent.RemoveListener(GetItem);
                }
            }

        }

        bool InArea()
        {
            return Physics.CheckSphere(transform.position + m_colliderOffset, m_colliderRadius, m_playerLayerMask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = InArea() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position + m_colliderOffset, m_colliderRadius);
        }
    }
}