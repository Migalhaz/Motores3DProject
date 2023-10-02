using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(fileName = "NewAmmoBox", menuName = "ScriptableObject/Item/New Ammo Box")]
    public class AmmoBox : AbstractItem
    {
        [SerializeField, Min(1)] int m_ammoValue = 1;
        public int m_AmmoValue => m_ammoValue;
    }
}