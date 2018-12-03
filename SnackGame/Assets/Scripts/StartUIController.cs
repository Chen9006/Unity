using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour
{
    public Text lastText;
    public Text bestText;

    public Toggle blue;
    public Toggle yellow;
    public Toggle border;
    public Toggle free;

    void Awake()
    {
        lastText.text = "上次 : 长度 " + PlayerPrefs.GetInt("lastLength", 0) + " , 分数 " + PlayerPrefs.GetInt("lastScore", 0);
        bestText.text = "最佳 : 长度 " + PlayerPrefs.GetInt("TheBestLength", 0) + " , 分数 " + PlayerPrefs.GetInt("TheBestScore", 0);
    }

     void Start()
    {
        if (PlayerPrefs.GetString("sh", "sh01") == "sh01")
        {
            blue.isOn = true;
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
        else
        {
            yellow.isOn = true;
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }

        if (PlayerPrefs.GetInt("Border", 1) == 1)
        {
            border.isOn = true;
            PlayerPrefs.SetInt("Border", 1);
        }
        else
        {
            free.isOn = true;
            PlayerPrefs.SetInt("Border", 0);
        }

    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void BlueSelected(bool isOn)
    {
        if(isOn)
        {
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
    }

    public void YellowSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }
    }

    public void BorderSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Border", 1);
                 
        }
    }

    public void FreeSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Border", 0);
        }
    }

}
