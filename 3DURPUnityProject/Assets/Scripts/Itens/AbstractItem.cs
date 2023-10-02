using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item
{
    public abstract class AbstractItem : ScriptableObject
    {
        [SerializeField] string m_itemName;
        [SerializeField, Min(0)] float m_itemPrice;
        [SerializeField, Min(1)] int m_itemSpace = 1;

        public string m_ItemName => m_itemName;
        public float m_ItemPrice => m_itemPrice;
        public int m_ItemSpace => m_itemSpace;
    }
}