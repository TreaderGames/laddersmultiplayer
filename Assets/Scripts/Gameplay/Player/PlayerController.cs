using System;
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

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_PHOTON_PLAYER_DISCONNECTED, HandlePlayerDisconnected);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_PHOTON_PLAYER_DISCONNECTED, HandlePlayerDisconnected);
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

            if (GlobalVariables.pCurrentGameState.Equals(GameStates.Multiplayer))
            {
                if (i == 0)
                {
                    playerBase.SetIsLocalPlayer(NetworkHandler.pInstance.GetIsMaster());
                }
                else
                {
                    playerBase.SetIsLocalPlayer(!NetworkHandler.pInstance.GetIsMaster());
                }
            }
        }
    }
    #endregion

    #region EventHandlers

    private void HandlePlayerDisconnected(object arg)
    {
        GlobalVariables.pIsLocalPlayerWin = !(bool)arg;
        ScreenLoader.Instance.LoadScreen(ScreenType.GameOver, null);
    }
    #endregion
}
