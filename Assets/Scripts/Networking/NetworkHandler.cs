using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public enum EventCodes
{
    START_GAME,
    ROLL_RESULT,
}

public class NetworkHandler : NetworkBase
{
    public static NetworkHandler pInstance { get; private set; }

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
    #endregion

    #region Public

    public void SendResultOfRoll(int result)
    {
        RaisePhotonEvent(EventCodes.ROLL_RESULT, result, ReceiverGroup.All);
    }

    #endregion

    #region EventHandlers

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

    protected override void OnEvent(EventData eventData)
    {
        base.OnEvent(eventData);
        byte eventCode = eventData.Code;
        if (eventCode == (byte)EventCodes.START_GAME)
        {
            EventController.TriggerEvent(EventID.EVENT_PHOTON_ALL_PLAYERS_JOINED);
        }
        else if (eventCode == (byte)EventCodes.ROLL_RESULT)
        {
            EventController.TriggerEvent(EventID.EVENT_DICE_ROLLED, (int)eventData.CustomData);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        EventController.TriggerEvent(EventID.EVENT_PHOTON_PLAYER_DISCONNECTED, otherPlayer.IsLocal);
    }
    #endregion
}
