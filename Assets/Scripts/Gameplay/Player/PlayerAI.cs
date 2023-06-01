using System;
using System.Collections;
using UnityEngine;

public class PlayerAI : PlayerBase
{
    [SerializeField] float turnDelay; //This can be a random value

    #region Unity

    protected override void Start()
    {
        base.Start();
        TryTakeTurnAtStart();
    }

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

    #region Private
    private IEnumerator DelayedEnemyTurn()
    {
        yield return new WaitForSeconds(turnDelay);
        EventController.TriggerEvent(EventID.EVENT_DICE_ROLLED, UnityEngine.Random.Range(1, 7));
    }

    private void TryTakeTurnAtStart()
    {
        if (!GlobalVariables.pIsLocalPlayerTurn)
        {
            StartCoroutine(DelayedEnemyTurn());
        }
    }
    #endregion

    #region EventHandlers

    private void HandleTurnEnd(object arg)
    {
        if ((bool)arg)
        {
            StartCoroutine(DelayedEnemyTurn());
        }
    }

    #endregion
}
