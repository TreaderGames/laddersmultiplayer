using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworking : PlayerBase
{
    #region EventHandlers
    protected override void HandleDiceRolled(object arg)
    {
        NetworkHandler.pInstance.SendResultOfRoll((int)arg);
        base.HandleDiceRolled(arg);
    }
    #endregion
}
