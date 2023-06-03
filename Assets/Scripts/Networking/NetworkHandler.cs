using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkHandler : MonoBehaviourPunCallbacks
{
    private static byte playerCount;
    #region Public

    public static void StartConnectionAndJoinRoom()
    {
        playerCount = 0;
        PhotonNetwork.ConnectUsingSettings();
    }

    #endregion

    #region EventHandlers
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Photon Connected");
        PhotonNetwork.JoinRandomOrCreateRoom(null, LaddersConfig.MAX_PLAYERS);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Photon Joined Room");
        playerCount++;
        if(playerCount.Equals(LaddersConfig.MAX_PLAYERS))
        {
            EventController.TriggerEvent(EventID.EVENT_PHOTON_ALL_PLAYERS_JOINED);
        }
    }
    #endregion
}
