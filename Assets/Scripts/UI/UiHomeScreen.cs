using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHomeScreen : MonoBehaviour
{
    #region Public
    public void OnSinglePlayerClicked()
    {
        SceneManager.LoadSceneAsync(LaddersConfig.GAMEPLAY_SCENE);
    }
    #endregion
}
