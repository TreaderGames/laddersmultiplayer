using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : Singleton<TurnHandler>
{
    private bool isLocalPlayerTurn = true;

    public bool pIsLocalPlayerTurn { get => isLocalPlayerTurn; }

    #region Unity
    public void OnEnable()
    {
        isLocalPlayerTurn = true;
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    public void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    #endregion

    #region EventHandlers
    private void HandleTurnEnd(object arg)
    {
        isLocalPlayerTurn = !isLocalPlayerTurn;
    }
    #endregion
}
