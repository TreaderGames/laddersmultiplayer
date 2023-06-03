using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] bool isLocalPlayer;
    [SerializeField] TileDataCollection tileData;
    [SerializeField] Vector3 standbyPosition;

    private Dictionary<int, int> tileDataCollection = new Dictionary<int, int>();

    private int currentTileCount = 0;
    #region Unity
    protected virtual void Start()
    {
        tileDataCollection = tileData.GetTileDataCollection();
    }

    protected virtual void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_DICE_ROLLED, HandleDiceRolled);
        EventController.StartListening(EventID.EVENT_RESTART, HandleRestart);
    }

    protected virtual void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_DICE_ROLLED, HandleDiceRolled);
        EventController.StopListening(EventID.EVENT_RESTART, HandleRestart);
    }

    #endregion

    #region Public
    public void SetIsLocalPlayer(bool value)
    {
        isLocalPlayer = value;
    }

    public void SetToStandByPosition()
    {
        transform.position = standbyPosition;
    }
    #endregion

    #region Private
    private void ResolveTurn(int result)
    {
        int tilesCount = GridTilesBuilder.Instance.GetTilesCount();
        currentTileCount += result;
        if (currentTileCount > tilesCount)
        {
            currentTileCount -= result;
            EventController.TriggerEvent(EventID.EVENT_TURN_END, isLocalPlayer);
            return;
        }
        else if (currentTileCount.Equals(tilesCount))
        {
            GlobalVariables.pIsLocalPlayerWin = isLocalPlayer;
            ScreenLoader.Instance.LoadScreen(ScreenType.GameOver, null);
        }

        MovePlayer();
        EventController.TriggerEvent(EventID.EVENT_TURN_END, isLocalPlayer);
    }

    private void MovePlayer()
    {
        if (tileDataCollection.ContainsKey(currentTileCount))
        {
            currentTileCount = tileDataCollection[currentTileCount];
        }

        transform.localPosition = GridTilesBuilder.Instance.GetPositionForTile(currentTileCount);
    }
    #endregion

    #region EventHandlers
    protected virtual void HandleDiceRolled(object arg)
    {
        if(isLocalPlayer == GlobalVariables.pIsLocalPlayerTurn)
        {
            ResolveTurn((int)arg);
        }
    }

    private void HandleRestart(object arg)
    {
        transform.position = standbyPosition;
        currentTileCount = 0;
    }

    #endregion
}
