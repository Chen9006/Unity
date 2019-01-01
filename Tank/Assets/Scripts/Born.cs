using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerTwoPrefab;

    public GameObject[] enemyPrefabList;

    public bool createPlayerOne;
    public bool createPlayerTwo;


    //单例(需要在Awake（）里面，添加一句instance = this;才可使用！)
    private static Born instance;

    public static Born Instance
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
    }

    // Use this for initialization
    void Start()
    {
        Invoke("bornTank", 0.8f);
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void bornTank()
    {
        if (createPlayerOne || createPlayerTwo)
        {
            if (createPlayerOne)
                Instantiate(playerPrefab, transform.position, Quaternion.identity);

            if (createPlayerTwo)
                Instantiate(playerTwoPrefab, transform.position, Quaternion.identity);
        }

        else
        {
            int num = Random.Range(1, 11);
            GameObject go;

            if (num <= 2) //奖励坦克
            {
                int type = Random.Range(3, 5);
                go = Instantiate(enemyPrefabList[type], transform.position, Quaternion.identity);
            }
            else  //普通坦克
            {
                int type = Random.Range(0, 3);
                go = Instantiate(enemyPrefabList[type], transform.position, Quaternion.identity);
            }


            MapCreation.Instance.enemyList.Add(go);

            //  Debug.Log(MapCreation.Instance.enemyList.Count);
        }
    }

}
