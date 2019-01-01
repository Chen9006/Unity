using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 3f;
    private Vector3 bulletEulerAngles;


    private SpriteRenderer sr;
    public Sprite[] tankSprite; //顺序 下0 右1 上 2 左3
    public GameObject bulletPrefabs;
    public GameObject explosionPrefab;

    public bool isTimeStop = false;

    //计时器
    private float timeVal;
    private float timeValChangeDirection = 0f;


    private float v = -1;
    private float h;

    //单例(需要在Awake（）里面，添加一句instance = this;才可使用！)
    private static Enemy instance;

    public static Enemy Instance
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
        sr = GetComponent<SpriteRenderer>();
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //攻击时间间隔
        if (timeVal >= 2f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        Move();
    }


    //坦克攻击方法
    private void Attack()
    {
        Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }

    //敌人坦克的移动方法
    private void Move()
    {
        if (timeValChangeDirection >= 3)
        {
            int num = Random.Range(0, 8);

            if (num<=2) //向前走
            {
                v = -1;
                h = 0;
            }
            else if (num == 7) //向后走
            {
                v = 1;
                h = 0;
            }
            else if (num >=3 && num <= 4)//向左走
            {
                v = 0;
                h = -1;
            }
            else if (num >= 5 && num <= 6)//向右走
            {
                v = 0;
                h = 1;
            }

            timeValChangeDirection = 0;
        }

        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0) //往下走
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0) //往上走
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }

        if (v != 0)
        {
            return;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (h < 0) //往左走
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0) //往右走
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

    }

    //玩家1消灭敌人坦克
    private void PlayerOneDefeatTank()
    {
        PlayManager.Instance.playerOneScore += 100;
        //爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    //玩家2消灭敌人坦克
    private void  PlayerTwoDefeatTank()
    {
        PlayManager.Instance.playerTwoScore += 100;
        //爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //玩家1消灭奖励坦克
    private void PlayerOneDefeaBonusTank()
    {
        PlayManager.Instance.playerOneScore += 200;
        //爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //玩家2消灭奖励坦克
    private void PlayerTwoDefeaBonusTank()
    {
        PlayManager.Instance.playerTwoScore += 200;
        //爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timeValChangeDirection = 3f;

            if (h == 0)
            {
                v = 0;
                int num = Random.Range(0, 2);
                if (num == 0) h = 1;
                else
                    h = -1;
            }

            if (v == 0)
            {
                h = 0;
                int num = Random.Range(0, 2);
                if (num == 0) v = 1;
                else
                    v = -1;
            }
        }

    }

}
