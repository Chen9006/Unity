using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class SnackHead : MonoBehaviour
{

    public List<Transform> BodyList = new List<Transform>();

    public float velocity = 0.35f; // 每隔多久调用一次Move方法
    public int step;
    private int x;     //坐标x移动增量
    private int y;     //坐标y移动增量
    private Vector3 headPos;

    private Transform canvas;

    public GameObject bodyPrefabs; //蛇身预制体
    public Sprite[] bodySprites = new Sprite[2];

    private bool isDie = false;
    public GameObject DieEffect;

    public AudioClip eatClip;
    public AudioClip dieClip;

    public GameObject GameOverPanel;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;

        //通过Resources.Load(string path)方法加载资源，path的书写不需要加Resources/以及文件扩展名
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh02"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0201"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0202"));

    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Move", 0, velocity); //重复调用Move方法
        x = step;
        y = 0;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90); //开始蛇头朝右
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && MainUIController.Instance.isPause == false  && isDie==false) //按下空格键加速
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity - 0.2f);
        }

        if (Input.GetKeyUp(KeyCode.Space) && MainUIController.Instance.isPause == false && isDie == false) //弹出复原
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }

        if (Input.GetKey(KeyCode.W) && y != -step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;
            y = step;
        }

        if (Input.GetKey(KeyCode.S) && y != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }

        if (Input.GetKey(KeyCode.A) && x != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }

        if (Input.GetKey(KeyCode.D) && x != -step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }

    }

    void Move()
    {
        headPos = gameObject.transform.localPosition; //蛇头移动前的位置
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y + headPos.z); //蛇头向期望方向移动


        if (BodyList.Count > 0)
        {
            //BodyList.Last().localPosition = headPos;
            //BodyList.Insert(0, BodyList.Last());
            //BodyList.RemoveAt(BodyList.Count - 1);

            //从后往前移动蛇身
            for (int i = BodyList.Count - 2; i >= 0; i--)
            {
                BodyList[i + 1].localPosition = BodyList[i].localPosition;
                //每一个蛇身移动到前一个蛇身的位置
            }

            BodyList[0].localPosition = headPos; //第一个蛇身移动到蛇头位置
        }

    }

    void Grow() //蛇身增长
    {
        AudioSource.PlayClipAtPoint(eatClip,Vector3.zero);

        int index = (BodyList.Count % 2 == 0) ? 0 : 1;
        GameObject body = Instantiate(bodyPrefabs, new Vector3(2000, 2000, 0), Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[index];
        body.transform.SetParent(canvas, false);

        BodyList.Add(body.transform);
    }

    void  Die()
    {

        AudioSource.PlayClipAtPoint(dieClip, Vector3.zero);

        CancelInvoke();
        isDie = true;
        Instantiate(DieEffect);

        /*记录得分*/

        PlayerPrefs.SetInt("lastLength",MainUIController.Instance.length);
        PlayerPrefs.SetInt("lastScore", MainUIController.Instance.score);

        if(PlayerPrefs.GetInt("TheBestScore", 0)< MainUIController.Instance.score)
        {

            PlayerPrefs.SetInt("TheBestLength", MainUIController.Instance.length);
            PlayerPrefs.SetInt("TheBestScore", MainUIController.Instance.score);
        }

        GameOverPanel.SetActive(true);

        StartCoroutine(GameOver(3.0f)); //3秒后重开游戏
    }

    IEnumerator GameOver(float t) //协程
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //也可写成collision.tag=="Food"
        if (collision.gameObject.CompareTag("Food")) //吃食物
        {
            Destroy(collision.gameObject);

            MainUIController.Instance.updateUI(); //更新分数长度信息

            Grow(); //吃完东西蛇身增长

            if (Random.Range(0, 100) <=20)
            {
                FoodMaker.Instance.MakeFood(true);
            }

            else
            {
                FoodMaker.Instance.MakeFood(false);
            }

        }

        else if(collision.gameObject.CompareTag("Reward")) //奖励
        {
            Destroy(collision.gameObject);

            MainUIController.Instance.updateUI(Random.Range(5,15)*10); //更新分数长度信息

            Grow(); //吃完东西蛇身增长
        }

        else if (collision.gameObject.CompareTag("Body"))
        {
            Die();
        }

        else
        {
            if(MainUIController.Instance.hasBorder )
            {
                Die();
            }
            else
            {
                switch (collision.gameObject.name) //自由模式
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
                        break;

                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                        break;

                    case "Left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z);
                        break;

                    case "Right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 240, transform.localPosition.y, transform.localPosition.z);
                        break;
                }

            }

        }

    }

}
