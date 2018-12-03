using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour
{

    private static FoodMaker instance;
    public static FoodMaker Instance
    {
        get
        {
            return instance;
        }
    }

    public int xLimit = 16;
    public int yLimit = 11;
    public int xOffset = 6;
    public GameObject foodPrefabs;
    public GameObject RewardPrefabs;
    public Sprite[] foodSprites;
    private Transform foodHolder;


    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        MakeFood(false);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void MakeFood(bool isReward)
    {
        int index = Random.Range(0, foodSprites.Length-1);
        GameObject food = Instantiate(foodPrefabs);
        food.GetComponent<Image>().sprite = foodSprites[index];
        food.transform.SetParent(foodHolder, false);
        int x = Random.Range(-xLimit + xOffset, xLimit);
        int y = Random.Range(-yLimit , yLimit);
        food.transform.localPosition = new Vector3(x * 30, y * 30, 0);

        if(isReward)
        {
            GameObject reward = Instantiate(RewardPrefabs);
            reward.transform.SetParent(foodHolder, false);
            x = Random.Range(-xLimit + xOffset, xLimit);
            y = Random.Range(-yLimit, yLimit);
            reward.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        }

    }
}
