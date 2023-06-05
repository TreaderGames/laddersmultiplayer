using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] string winText;
    [SerializeField] string loseText;
    [SerializeField] Text gameOverText;
    [SerializeField] Button restartButton;


    #region Unity
    private void Start()
    {
        gameOverText.text = GlobalVariables.pIsLocalPlayerWin ? winText : loseText;
        restartButton.gameObject.SetActive(GlobalVariables.pCurrentGameState.Equals(GameStates.SinglePlayer));
    }
    #endregion
    #region Public

    public void OnClickHome()
    {
        if(GlobalVariables.pCurrentGameState.Equals(GameStates.Multiplayer))
        {
            NetworkHandler.pInstance.Disconnect();
        }
        SceneManager.LoadSceneAsync(LaddersConfig.HOME_SCENE);
    }

    public void OnClickRestart()
    {
        EventController.TriggerEvent(EventID.EVENT_RESTART);
        Destroy(gameObject);
    }
    #endregion
}
