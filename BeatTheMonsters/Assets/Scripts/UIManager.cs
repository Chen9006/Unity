using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public Text shootNumText;
    public Text scoreText;
    public Text TheRestTime;
    public Text TheBestScore;

    public int shootNum = 0;
    public int score = 0;
    public int restTime = 30;

    public Toggle musicToggle;
    public AudioSource musicAudio;

    public Text messageText;

    private void Awake()
    {
        _instance = this;

        TheRestTime.text = "30";
        TheBestScore.text = PlayerPrefs.GetInt("TheBestScore", 0).ToString();

        if (PlayerPrefs.HasKey("MusicOn"))
        {
            if(PlayerPrefs.GetInt("MusicOn")==1)
            {
                musicToggle.isOn = true;
                musicAudio.enabled = true;
            }
            else
            {
                musicToggle.isOn = false;
                musicAudio.enabled = false;
            }
        }

        else
        {
            musicToggle.isOn = true;
            musicAudio.enabled = true;
        }
    }


    private void Start()
    {
        StartCoroutine(countDown(restTime));
    }

    private void Update()
    {
        //更新射击数，分数，剩余时间显示
        shootNumText.text = shootNum.ToString();
        scoreText.text = score.ToString();
        TheRestTime.text = restTime.ToString();
    }

    public void MusicSwitch() //背景音乐开关
    {
        if(musicToggle.isOn == false)
        {
            musicAudio.enabled = false;
            //保存音乐开关状态，0表示关闭状态
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        else
        {
            musicAudio.enabled = true;
            PlayerPrefs.SetInt("MusicOn", 1);
        }

        PlayerPrefs.Save();
    }

    public void AddShootNum()
    {
        shootNum++;
    }

    public void AddScore()
    {
        score++;
    }

    public void showMessage(string str)
    {
        messageText.text = str;
    }

    IEnumerator countDown(int resTime)
    {
        while (restTime>= 1)
        {
            yield return new WaitForSeconds(1);
            restTime--;
        }
    }



}
