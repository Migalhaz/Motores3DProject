using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Item;

namespace Game.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField, Min(0)] int m_money; 
        [SerializeField, Min(0)] int m_maxInventorySpace; 
        [SerializeField] List<AbstractItem> m_currentItens;
        int m_currentInventorySpace;

        public List<AbstractItem> m_CurrentItens => m_currentItens;
        public int m_Money => m_money;

        public bool AddItem(AbstractItem _newItem)
        {
            if (m_currentInventorySpace >= m_maxInventorySpace) return false;
            m_currentItens.Add(_newItem);
            m_currentInventorySpace += _newItem.m_ItemSpace;
            return true;
        }

        public bool RemoveItem(AbstractItem _newItem)
        {
            if (!m_currentItens.Contains(_newItem)) return false;
            m_currentItens.Remove(_newItem);
            m_currentInventorySpace -= _newItem.m_ItemSpace;
            return true;
        }

        public void AddMoney(int _value)
        {
            m_money += _value;
        }

        public void RemoveMoney(int _value)
        {
            m_money -= _value;
        }
    }
}