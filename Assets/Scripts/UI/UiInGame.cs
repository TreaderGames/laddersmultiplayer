using UnityEngine.UI;
using UnityEngine;

public class UiInGame : MonoBehaviour
{
    [SerializeField] Text resultText;
    [SerializeField] Text turnText;

    [SerializeField] Button rollButton;

    [SerializeField] string opponentTurnText;
    [SerializeField] string playerTurnText;

    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        resultText.text = "-";
        SetButtonState(GlobalVariables.pIsLocalPlayerTurn);
        UpdateTurnText(GlobalVariables.pIsLocalPlayerTurn);
    }

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    #endregion

    #region Private
    private void SetButtonState(bool state)
    {
        rollButton.interactable = state;
    }
    #endregion

    #region Public
    public void OnRollClick()
    {
        int diceSixRoll = Random.Range(1, 7);
        resultText.text = diceSixRoll.ToString();
        SetButtonState(false);
        EventController.TriggerEvent(EventID.EVENT_DICE_ROLLED, diceSixRoll);
        UpdateTurnText(false);
    }
    #endregion

    #region EventHandlers
    private void HandleTurnEnd(object arg)
    {
        SetButtonState(!(bool)arg);
        UpdateTurnText(!(bool)arg);
    }

    private void UpdateTurnText(bool localPlayerTurn)
    {
        turnText.text = localPlayerTurn ? playerTurnText : opponentTurnText;
    }
    #endregion
}
