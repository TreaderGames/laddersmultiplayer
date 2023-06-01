using System;
using UnityEngine;

public class PlayerAI : PlayerBase
{
    #region Unity
    protected override void OnEnable()
    {
        base.OnEnable();
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventController.StopListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    #endregion

    #region EventHandlers

    private void HandleTurnEnd(object arg)
    {
        bool wasLocalPlayer = (bool)arg;
        if(wasLocalPlayer)
        {
            EventController.TriggerEvent(EventID.EVENT_DICE_ROLLED, UnityEngine.Random.Range(1, 7));
        }
    }
    #endregion
}
