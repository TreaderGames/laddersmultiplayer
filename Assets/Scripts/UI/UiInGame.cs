using UnityEngine.UI;
using UnityEngine;

public class UiInGame : MonoBehaviour
{
    [SerializeField] Text resultText;
    [SerializeField] Button rollButton;

    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        resultText.text = "-";
        SetButtonState(TurnHandler.Instance.pIsLocalPlayerTurn);
    }

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
    }

    private void OnDisable()
    {
        EventController.StartListening(EventID.EVENT_TURN_END, HandleTurnEnd);
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
    }
    #endregion

    #region EventHandlers
    private void HandleTurnEnd(object arg)
    {
        SetButtonState(true);
    }
    #endregion
}
