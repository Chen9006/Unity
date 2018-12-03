using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinManager : MonoBehaviour
{

    private Transform startPoint;
    private Transform spawnPoint;
    public GameObject PinPrefabs;

    private PinController currentPin;

    private bool isGameOver = false;

    private int score = 0;
    public Text scoreText;

    private Camera mainCamera;
    public float AnimationSpeed = 3f;

    // Use this for initialization
    void Start()
    {
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        SpawnPin();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            score++;
            scoreText.text = score.ToString();
            currentPin.StartFly();
            SpawnPin();
        }
    }

    void SpawnPin() //实例化一枚针
    {
        currentPin = GameObject.Instantiate(PinPrefabs, spawnPoint.position, PinPrefabs.transform.rotation).GetComponent<PinController>();
    }

    public void GameOver()
    {
        if (isGameOver) return;

        GameObject.Find("Circle").GetComponent<RotateCircle>().enabled = false;

        isGameOver = true;

        StartCoroutine(GameOverAnimation());
    }

    IEnumerator GameOverAnimation()
    {
        /*游戏结束界面颜色渐变,尺寸变化*/
        while(true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor,Color.red,AnimationSpeed*Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize,4, AnimationSpeed * Time.deltaTime);

            if(Mathf.Abs(mainCamera.orthographicSize-4)<0.01)
            {
                break;
            }

            yield return 0;
        }

        yield return new WaitForSeconds(0.5f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}