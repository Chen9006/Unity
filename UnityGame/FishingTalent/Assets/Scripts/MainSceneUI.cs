using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public GameObject settingPanel;
    public Toggle muteToggle;

    void Start()
    {
        muteToggle.isOn = ! AudioManager._instance.IsMute;
    }

    public void  SwitchMute(bool isOn)
    {
        AudioManager._instance.switchMuteState(isOn);
    }

    public void OnBackButtonDown()
    {
        //保存当前游戏
        PlayerPrefs.SetInt("gold",GameController._instance.gold);
        PlayerPrefs.SetInt("lv", GameController._instance.LV);
        PlayerPrefs.SetFloat("bonusCountDown", GameController._instance.timer);
        PlayerPrefs.SetFloat("rewardCountDown", GameController._instance.rewardTimer);
        PlayerPrefs.SetInt("exp", GameController._instance.EXP);
        int temp = (AudioManager._instance.IsMute == false) ? 0 : 1;
        PlayerPrefs.SetInt("mute", temp);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void OnSettingButtonDown()
    {
        settingPanel.SetActive(true);
    }

    public void OnCloseSettingButtonDown()
    {
        settingPanel.SetActive(false);
    }
}
