using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkBase : MonoBehaviourPunCallbacks
{
    public bool GetIsMaster() { return PhotonNetwork.IsMasterClient; }

    #region Unity

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

    #region Protected
    protected void RaisePhotonEvent(EventCodes eventCodes, object parameters, ReceiverGroup receiverGroup)
    {
        Debug.Log("RaisePhotonEvent" + eventCodes.ToString());
        byte eventCode = (byte)eventCodes;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = receiverGroup };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(eventCode, parameters, raiseEventOptions, sendOptions);
    }

    #endregion

    #region Private
    private void JoinRandomRoom()
    {
        Debug.Log("Photon Connected");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = LaddersConfig.MAX_PLAYERS;
        roomOptions.PlayerTtl = 600;
        roomOptions.EmptyRoomTtl = 600;
        roomOptions.PublishUserId = true;
        roomOptions.CleanupCacheOnLeave = true;
        PhotonNetwork.JoinRandomOrCreateRoom(null, LaddersConfig.MAX_PLAYERS, MatchmakingMode.FillRoom, null, null, null, roomOptions);
    }
    #endregion

    #region Public

    public void StartConnectionAndJoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            JoinRandomRoom();
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    #endregion

    #region EventHandlers

    protected virtual void OnEvent(EventData obj) { }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Photon Joined Room");
    }
    #endregion
}
