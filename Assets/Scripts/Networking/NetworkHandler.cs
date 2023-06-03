using Photon.Pun;

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
        PhotonNetwork.JoinRandomOrCreateRoom(null, LaddersConfig.MAX_PLAYERS);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        playerCount++;
        if(playerCount.Equals(LaddersConfig.MAX_PLAYERS))
        {
            EventController.TriggerEvent(EventID.EVENT_PHOTON_ALL_PLAYERS_JOINED);
        }
    }
    #endregion
}
