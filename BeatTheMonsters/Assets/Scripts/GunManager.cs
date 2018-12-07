using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    /*最大，最小的X，Y轴的旋转角度*/
    private float maxYRotation = 120;
    private float minYRotation = 0;
    private float maxXRotation = 80;
    private float minXRotation = 0;

    private float shootTime = 0.5f;//射击时间间隔
    private float shootTimer = 0;//射击间隔时间计时器

    public GameObject bulletGameObject;
    public Transform firePosition;

    private AudioSource gunAudio;

    private void Awake()
    {
        gunAudio = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {


        if (GameManager._instance.isPause == false) /*游戏是非暂停状态，才可控制移动枪*/
        {
            shootTimer = shootTimer + Time.deltaTime;

            if (shootTimer >= shootTime)
            {
                /*左键射击*/
                if (Input.GetMouseButtonDown(0))
                {
                    //实例化子弹
                    GameObject bulletCurrent = GameObject.Instantiate(bulletGameObject, firePosition.position, Quaternion.identity);
                    //通过刚体组件给子弹施加正前方向力，使其运动
                    bulletCurrent.GetComponent<Rigidbody>().AddForce(transform.forward * 3500);
                    //播放手机开火动画
                    gameObject.GetComponent<Animation>().Play();

                    shootTimer = 0;

                    //播放开火音效
                    gunAudio.Play();

                    //更新射击数
                    UIManager._instance.AddShootNum();


                }

            }

            /*根据鼠标的位置旋转手枪*/
            float xPosPercent = Input.mousePosition.x / Screen.width;
            float yPosPercent = Input.mousePosition.y / Screen.height;

            float xAngle = -Mathf.Clamp(yPosPercent * maxXRotation, minXRotation, maxXRotation) + 20;
            float yAngle = Mathf.Clamp(xPosPercent * maxYRotation, minYRotation, maxYRotation) - 60 + 180;


            transform.eulerAngles = new Vector3(xAngle, yAngle, 0);

        }


    }



}
