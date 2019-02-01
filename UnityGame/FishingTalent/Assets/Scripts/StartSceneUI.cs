using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneUI : MonoBehaviour
{
    public void newGame()
    {
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("lv");
        PlayerPrefs.DeleteKey("bonusCountDown");
        PlayerPrefs.DeleteKey("rewardCountDown");
        PlayerPrefs.DeleteKey("exp");
      
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void loadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void onCloseButtonDown()
    {
        Application.Quit();
    }

}
