using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerBase[] singlePlayerGroup;
    [SerializeField] PlayerBase[] multiplayerGroup;
    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        InitiateBasedOnGameMode();
    }
    #endregion

    #region Private
    private void InitiateBasedOnGameMode()
    {
        PlayerBase[] playerGroup = GlobalVariables.pCurrentGameState.Equals(GameStates.SinglePlayer) ? singlePlayerGroup : multiplayerGroup;
        PlayerBase playerBase;
        for (int i = 0; i < playerGroup.Length; i++)
        {
            playerBase = Instantiate<PlayerBase>(playerGroup[i], transform);
            playerBase.SetToStandByPosition();
        }

        if (GlobalVariables.pCurrentGameState.Equals(GameStates.Multiplayer))
        {
            playerGroup[0].SetIsLocalPlayer(NetworkHandler.pInstance.GetIsMaster());
        }
    }
    #endregion
}
