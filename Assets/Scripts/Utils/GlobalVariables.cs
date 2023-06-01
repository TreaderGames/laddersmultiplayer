using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Home,
    SinglePlayer,
    Multiplayer
}

public class GlobalVariables
{
    public static GameStates pCurrentGameState { get; set; }
}
