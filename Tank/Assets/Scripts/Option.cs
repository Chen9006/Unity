using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public int choice = 1;
    public Transform posOne;
    public Transform posTwo;

    //单例(需要在Awake（）里面，添加一句instance = this;才可使用！)
    private static Option instance;

    public static Option Instance
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
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            choice = 1;
            transform.position = posOne.position;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            choice = 2;
            transform.position = posTwo.position;
        }

        if(choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        if (choice == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

    }
}
