using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHomeScreen : MonoBehaviour
{
    #region Unity
    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_PHOTON_ALL_PLAYERS_JOINED, HandleAllPlayersJoinedRoom);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_PHOTON_ALL_PLAYERS_JOINED, HandleAllPlayersJoinedRoom);
    }
    #endregion
    #region Public
    public void OnSinglePlayerClicked()
    {
        GlobalVariables.pIsLocalPlayerTurn = true;
        GlobalVariables.pCurrentGameState = GameStates.SinglePlayer;
        SceneManager.LoadSceneAsync(LaddersConfig.GAMEPLAY_SCENE);
    }

    public void OnClickMultiplayer()
    {
        GlobalVariables.pCurrentGameState = GameStates.Multiplayer;
        NetworkHandler.pInstance.StartConnectionAndJoinRoom();
    }

    #endregion

    #region EventHandlers
    private void HandleAllPlayersJoinedRoom(object arg)
    {
        GlobalVariables.pIsLocalPlayerTurn = NetworkHandler.pInstance.GetIsMaster();
        SceneManager.LoadSceneAsync(LaddersConfig.GAMEPLAY_SCENE);
    }
    #endregion
}
