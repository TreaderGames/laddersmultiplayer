using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public enum EventCodes
{
    START_GAME,
}

public class NetworkHandler : MonoBehaviourPunCallbacks
{
    public static NetworkHandler pInstance { get; private set; }
    public bool GetIsMaster() { return PhotonNetwork.IsMasterClient; }

    #region Unity
    private void Start()
    {
        #region SingletonLogic

        if (pInstance == null)
        {
            pInstance = this;
        }
        else
        {
            if (pInstance != this)
            {
                Destroy(pInstance.gameObject);
                pInstance = null;
            }
        }

        #endregion
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    #endregion

    #region Public

    public static void StartConnectionAndJoinRoom()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    #endregion

    #region Private
    protected void RaisePhotonEvent(EventCodes eventCodes, object parameters, ReceiverGroup receiverGroup)
    {
        Debug.Log("RaisePhotonEvent" + eventCodes.ToString());
        byte eventCode = (byte)eventCodes;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = receiverGroup };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(eventCode, parameters, raiseEventOptions, sendOptions);
    }
    #endregion

    #region EventHandlers
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Photon Connected");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = LaddersConfig.MAX_PLAYERS;
        roomOptions.PlayerTtl = 600;
        roomOptions.EmptyRoomTtl = 600;
        roomOptions.PublishUserId = true;
        roomOptions.CleanupCacheOnLeave = true;
        PhotonNetwork.JoinRandomOrCreateRoom(null, LaddersConfig.MAX_PLAYERS, MatchmakingMode.FillRoom, null, null, null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Photon Joined Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Photon Entered Room");
        if (PhotonNetwork.CurrentRoom.PlayerCount == LaddersConfig.MAX_PLAYERS)
        {
            RaisePhotonEvent(EventCodes.START_GAME, "0", ReceiverGroup.All);
        }
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Photon Created Room");
    }

    private void OnEvent(EventData eventData)
    {
        byte eventCode = eventData.Code;
        if (eventCode == (byte)EventCodes.START_GAME)
        {
            EventController.TriggerEvent(EventID.EVENT_PHOTON_ALL_PLAYERS_JOINED);
        }
    }
    #endregion
}
