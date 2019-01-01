using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play2Controller : MonoBehaviour
{

    public float moveSpeed = 3f;
    private Vector3 bulletEulerAngles;
    private float timeVal;

    private float defendTimeVal = 5f;
    public bool isDefended = true; //无敌状态

    private SpriteRenderer sr;
    public Sprite[] tankSprite; //顺序 上0 右1 下 2 左3
    public GameObject bulletPrefabs;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;

    public AudioClip bonusAudio;

    //单例(需要在Awake（）里面，添加一句instance = this;才可使用！)
    private static Play2Controller instance;

    public static Play2Controller Instance
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
        sr = GetComponent<SpriteRenderer>();
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }

    }

    private void FixedUpdate()
    {
        if(PlayManager.Instance.playerOneisDefeat && PlayManager.Instance.playerTwoisDefeat) return ; //游戏结束

        Move();

        //攻击CD
        if (timeVal >= 0.3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    //坦克攻击方法
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;

        }
    }

    //坦克的移动方法
    private void Move()
    {
        float v = Input.GetAxisRaw("Play2_Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0) //往下走
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0) //往上走
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }


        if (v != 0)
        {
            return;
        }



        float h = Input.GetAxisRaw("Play2_Horizontal");
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

    //坦克的死亡方法
    private void Die()
    {
        if (isDefended) return;

        PlayManager.Instance.playTwoisDie = true;
        //爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bonus_AddLife":
                PlayManager.Instance.playerTwoLifeVal++;
                AudioSource.PlayClipAtPoint(bonusAudio, Vector3.zero);
                Destroy(collision.gameObject);
                break;

            case "Bonus_ ClearEnemy":
                MapCreation.Instance.clearAllEnemy();
                AudioSource.PlayClipAtPoint(bonusAudio, Vector3.zero);
                Destroy(collision.gameObject);
                break;

            case "Bonus_Build":
                MapCreation.Instance.buildTheBarrier();
                AudioSource.PlayClipAtPoint(bonusAudio, Vector3.zero);
                Destroy(collision.gameObject);
                break;

            case "Bonus_GetDefended":
                defendTimeVal = 8f;
                isDefended = true;
                AudioSource.PlayClipAtPoint(bonusAudio, Vector3.zero);
                Destroy(collision.gameObject);
                break;

            case "Bonus_ SpeedUp":
                moveSpeed = 5f;
                Invoke("setSpeed", 8f);
                AudioSource.PlayClipAtPoint(bonusAudio, Vector3.zero);
                Destroy(collision.gameObject);
                break;


        }
    }

    private void setSpeed()
    {
        moveSpeed = 3f;
    }
}
