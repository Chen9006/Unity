using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    //属性值
    public int playerOneLifeVal = 3;
    public int playerOneScore = 0;
    public bool playOneisDie = false;

    public int playerTwoLifeVal = 3;
    public int playerTwoScore = 0;
    public bool playTwoisDie = false;

    public bool playerOneisDefeat = false;
    public bool playerTwoisDefeat = false;

    //引用
    public GameObject born;
    public GameObject isDefeatUI;

    public Text playerOneScoreText;
    public Text playerOneLifeValText;

    public Text playerTwoScoreText;
    public Text playerTwoLifeValText;

    //
    public GameObject playerTwoScoreDisplay;
    public GameObject playerTwoLifeValDisplay;

    //单例
    private static PlayManager instance;

    public static PlayManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
        //单人模式不显示玩家2的数据
        if(Option.Instance.choice==1)
        {
            playerTwoisDefeat = true;
            playerTwoScoreDisplay.SetActive(false);
            playerTwoLifeValDisplay.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerOneisDefeat && playerTwoisDefeat)
        {
      //      Debug.Log("GAMEOVER");
            isDefeatUI.SetActive(true);
            Invoke("returnToMenu", 3); //延时3s返回主菜单
            return;
        }

        if (playOneisDie)
        {
            recover();
        }

        if (playTwoisDie)
        {
            recoverTankTwo();
        }

        //玩家1得分生命值
        playerOneScoreText.text = playerOneScore.ToString();
        playerOneLifeValText.text = playerOneLifeVal.ToString();
        //玩家2得分生命值
        playerTwoScoreText.text = playerTwoScore.ToString();
        playerTwoLifeValText.text = playerTwoLifeVal.ToString();
    }

    //玩家1复活
    private void recover()
    {
        if (playerOneLifeVal <= 0)
        {
            //玩家1失败
            playerOneisDefeat = true;
        //    Debug.Log("GAMEOVER1");
        }
        else
        {
            playerOneLifeVal--;

            if (playerOneLifeVal <= 0)
            {
                playerOneisDefeat = true;
            //    Debug.Log("GAMEOVER1");
                return;
            }

            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayerOne = true;
            playOneisDie = false;

        }
    }

    //玩家2复活
    private void recoverTankTwo()
    {
        if (playerTwoLifeVal <= 0)
        {
            //玩家2失败
            playerTwoisDefeat = true;
            Debug.Log("GAMEOVER2");
        }
        else
        {
            playerTwoLifeVal--;
            Debug.Log(playerTwoLifeVal);
            if (playerTwoLifeVal <= 0)
            {
                playerTwoisDefeat = true;
                Debug.Log("GAMEOVER2");
                return;
            }

            GameObject go = Instantiate(born, new Vector3(2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayerTwo = true;
            playTwoisDie = false;

        }
    }

    private void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
