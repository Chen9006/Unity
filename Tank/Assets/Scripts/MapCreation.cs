using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{

    /**用来装饰初始化地图所需物体的数组
     * 0家园堡垒 1墙 2岩石障碍 3出生效果 4河流 5草 6空气墙
     * 7-11增益效果
     * 7增加生命 8消灭所有敌人 9家园增强 10保护罩 11加速
    **/
    public GameObject[] item;

    //单例(需要在Awake（）里面，添加一句instance = this;才可使用！)
    private static MapCreation instance;

    //地图上已有物障碍的位置
    private List<Vector3> itemPositionList = new List<Vector3>();

    private List<GameObject> homeGameObject = new List<GameObject>();

    //存放敌人对象
    public List<GameObject> enemyList = new List<GameObject>();

    public static MapCreation Instance
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
        InitMap();
        instance = this;
    }

    private void InitMap()
    {
        GameObject go;
        //实例化家园
        Instantiate(item[0], new Vector3(0, -8, 0), Quaternion.identity);

        //用墙把家园围起来
        go = Instantiate(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        homeGameObject.Add(go);
        Instantiate(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        homeGameObject.Add(go);

        itemPositionList.Add(new Vector3(-1, -8, 0));
        itemPositionList.Add(new Vector3(1, -8, 0));

        for (int i = -1; i < 2; i++)
        {
            go = Instantiate(item[1], new Vector3(i, -7, 0), Quaternion.identity);
            homeGameObject.Add(go);
            itemPositionList.Add(new Vector3(i, -7, 0));
        }


        //实例化外围墙
        for (int i = -11; i < 12; i++)
        {
            createItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            createItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }

        for (int i = -8; i < 9; i++)
        {
            createItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
            createItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //初始化玩家1
        go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayerOne = true;

        //双人模式
        if(Option.Instance.choice == 2)
        {
            //初始化玩家2
            go = Instantiate(item[3], new Vector3(2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayerTwo = true;
        }
       

        //产生敌人
        createItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);

        createItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

        createItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);


        InvokeRepeating("createEnemy", 2f, 2f); //循环产生敌人

        //实例化墙
        for (int i = 0; i <40; i++)
        {
            createItem(item[1], createRandomPosition(), Quaternion.identity);
        }

        //实例化岩石障碍
        for (int i = 0; i < 15; i++)
        {
            createItem(item[2], createRandomPosition(), Quaternion.identity);
        }

        //实例化河流
        for (int i = 0; i < 10; i++)
        {
            createItem(item[4], createRandomPosition(), Quaternion.identity);
        }

        //实例化草
        for (int i = 0; i < 20; i++)
        {
            createItem(item[5], createRandomPosition(), Quaternion.identity);
        }

        //实例化河流
        for (int i = 0; i < 10; i++)
        {
            createItem(item[4], createRandomPosition(), Quaternion.identity);
        }

        //实例化岩石障碍
        for (int i = 0; i <15; i++)
        {
            createItem(item[2], createRandomPosition(), Quaternion.identity);
        }

        //实例化墙
        for (int i = 0; i < 40; i++)
        {
            createItem(item[1], createRandomPosition(), Quaternion.identity);
        }
    }

    private void createItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGO = Instantiate(createGameObject, createPosition, createRotation);
        itemGO.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }


    //产生随机位置
    private Vector3 createRandomPosition()
    {
        //不生成边界障碍
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);

            if (!hasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    //用来判断位置是否已有障碍
    private bool hasThePosition(Vector3 pos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (pos == itemPositionList[i]) return true;
        }

        return false;
    }


    //产生敌人
    private void createEnemy()
    {
        int num = Random.Range(0, 5);
        Vector3 pos = new Vector3();

        switch (num)
        {
            case 0:
                pos = new Vector3(-10, 8, 0);
                break;
            case 1:
                pos = new Vector3(10, 8, 0);
                break;
            case 2:
                pos = new Vector3(0, 8, 0);
                break;
            case 3:
                pos = new Vector3(-10, 0, 0);
                break;
            case 4:
                pos = new Vector3(10, 0, 0);
                break;
            default:
                break;
        }
        Instantiate(item[3], pos, Quaternion.identity);
    }

    //产生奖励图标
    public void createBonus()
    {
        int num = Random.Range(7, 12);
        GameObject go= Instantiate(item[num], createRandomPosition(), Quaternion.identity);

        StartCoroutine(delay(go));
    }

    //每个奖励8s后消失
    IEnumerator delay( GameObject gameObject)
    {
        yield return new WaitForSeconds(8.0f);
        Destroy(gameObject);
    }

    //将家园用墙围起来
    private void buildTheWall()
    {
        changeHomeMaterial();

        GameObject go;

        //用墙把家园围起来
        go = Instantiate(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        homeGameObject.Add(go);
        go = Instantiate(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        homeGameObject.Add(go);

        for (int i = -1; i < 2; i++)
        {
            go = Instantiate(item[1], new Vector3(i, -7, 0), Quaternion.identity);
            homeGameObject.Add(go);
        }

    }

    //将家园用岩石障碍围起来，延时5s
    public void buildTheBarrier()
    {
        changeHomeMaterial();

        GameObject go;

        //用用岩石障碍把家园围起来
        go = Instantiate(item[2], new Vector3(-1, -8, 0), Quaternion.identity);
        homeGameObject.Add(go);
        go = Instantiate(item[2], new Vector3(1, -8, 0), Quaternion.identity);
        homeGameObject.Add(go);

        for (int i = -1; i < 2; i++)
        {
            go = Instantiate(item[2], new Vector3(i, -7, 0), Quaternion.identity);
            homeGameObject.Add(go);
        }

        Invoke("buildTheWall", 5);
    }

    //更换家园的堡垒材料需要先删除之前的材料对象
    private void changeHomeMaterial()
    {
        for (int i = 0; i < homeGameObject.Count; i++)
        {
            Destroy(homeGameObject[i]);
        }
        homeGameObject.Clear();
    }

    //清除当前所有敌人
    public void clearAllEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            Destroy(enemyList[i]);
            PlayManager.Instance.playerOneScore += 50;
            PlayManager.Instance.playerTwoScore += 50;
            //爆炸特效
            Instantiate(Enemy.Instance.explosionPrefab, transform.position, transform.rotation);
        }
        enemyList.Clear();
    }
}
