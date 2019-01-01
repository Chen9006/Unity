using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;

    public bool isPlayerBullet;
    public bool isPlayerOneBullet; //true是玩家1发射的子弹，false是玩家2发射的子弹

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":

                if (!isPlayerBullet && (!Player.Instance.isDefended)) //敌人的子弹且不在无敌时间
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }

                break;

            case "TankTwo":
                if (!isPlayerBullet && (!Play2Controller.Instance.isDefended)) //敌人的子弹且不在无敌时间
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
                break;
                
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;

            case "Enemy":
                if (isPlayerBullet) //玩家的子弹
                {
                    if(isPlayerOneBullet)
                    {
                       collision.SendMessage("PlayerOneDefeatTank");
                    }
                    else
                    {
                        collision.SendMessage("PlayerTwoDefeatTank");
                    }

                    MapCreation.Instance.enemyList.Remove(collision.gameObject);
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }

                break;

            case "BonusEnemy":

                if (isPlayerBullet) //玩家的子弹
                {
                    if(isPlayerOneBullet)
                    {
                        collision.SendMessage("PlayerOneDefeaBonusTank");     
                    }
                    else
                    {
                        collision.SendMessage("PlayerTwoDefeaBonusTank");
                    }

                    MapCreation.Instance.enemyList.Remove(collision.gameObject);
                    MapCreation.Instance.createBonus();
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
              
                break;

            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;

            case "Barrier":
                // collision.SendMessage("playAudio");
                Destroy(gameObject);
                break;

            case "EnemyBullet": //子弹抵消
                Destroy(gameObject);
                Destroy(collision.gameObject);
                break;

            default:
                break;

        }
    }


}
