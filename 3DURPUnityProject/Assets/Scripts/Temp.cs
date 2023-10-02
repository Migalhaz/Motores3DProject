using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private void Start()
    {
        PlayerManager playerManager = PlayerManager.Instance;

        PlayerAnimatorController playerAnimatorController = playerManager.m_PlayerAnimatorController;
        PlayerAim playerAim = playerManager.m_PlayerAim;
        PlayerInputs playerInputs = playerManager.m_PlayerInputs;
        PlayerInventory playerInventory = playerManager.m_PlayerInventory;
        PlayerMove playerMove = playerManager.m_PlayerMove;

        Debug.Log(playerAnimatorController);
        Debug.Log(playerAim);
        Debug.Log(playerInputs);
        Debug.Log(playerInventory);
        Debug.Log(playerMove);
    }
}
