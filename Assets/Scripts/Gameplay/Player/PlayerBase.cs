using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] bool isLocalPlayer;
    [SerializeField] TileDataCollection tileData;

    private Dictionary<int, int> tileDataCollection = new Dictionary<int, int>();

    private int currentTileCount = 0;
    #region Unity
    private void Start()
    {
        tileDataCollection = tileData.GetTileDataCollection();
    }

    protected virtual void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_DICE_ROLLED, HandleDiceRolled);
    }

    protected virtual void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_DICE_ROLLED, HandleDiceRolled);
    }

    #endregion

    #region Protected
    #endregion

    #region Private
    protected virtual void ResolveTurn(int result)
    {
        int tilesCount = GridTilesBuilder.Instance.GetTilesCount();
        currentTileCount += result;
        if (currentTileCount > tilesCount)
        {
            currentTileCount -= result;
            EventController.TriggerEvent(EventID.EVENT_TURN_END, TurnHandler.Instance.pIsLocalPlayerTurn);
            return;
        }
        else if (currentTileCount.Equals(tilesCount))
        {
            //Handle Game win case
        }

        MovePlayer();
        EventController.TriggerEvent(EventID.EVENT_TURN_END, TurnHandler.Instance.pIsLocalPlayerTurn); //Passing the bool state at this point to avoid a potential race condition
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
    private void HandleDiceRolled(object arg)
    {
        if(isLocalPlayer == TurnHandler.Instance.pIsLocalPlayerTurn)
        {
            ResolveTurn((int)arg);
        }
    }
    #endregion
}
