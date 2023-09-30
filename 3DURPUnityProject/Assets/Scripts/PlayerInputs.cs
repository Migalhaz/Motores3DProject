using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [DisallowMultipleComponent()]
    public class PlayerInputs : MonoBehaviour
    {
        Vector2 m_moveInputs;
        public Vector2 m_MoveInputs => m_moveInputs;


        private void Update()
        {
            MoveInput();
        }

        void MoveInput()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            m_moveInputs.Set(x, y);
        }
    }
}