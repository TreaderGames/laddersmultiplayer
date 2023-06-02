using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] string winText;
    [SerializeField] string loseText;
    [SerializeField] Text gameOverText;


    #region Unity
    private void Start()
    {
        gameOverText.text = GlobalVariables.pIsLocalPlayerWin ? winText : loseText;
    }
    #endregion
    #region Public

    public void OnClickHome()
    {
        SceneManager.LoadSceneAsync(LaddersConfig.HOME_SCENE);
    }

    public void OnClickRestart()
    {
        EventController.TriggerEvent(EventID.EVENT_RESTART);
        Destroy(gameObject);
    }
    #endregion
}
