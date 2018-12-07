using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    //保存所有该目标下的怪物
    public GameObject[] monsters;
    //保存处于激活状态的怪物
    public GameObject activeMonster = null;


    public int targetPosition;//表示目标所在位置(0-8)



    private void Start()
    {
        foreach (GameObject monster in monsters)
        {
            monster.GetComponent<BoxCollider>().enabled = false;
            monster.SetActive(false);
        }

        //  ActivateMonster();
        StartCoroutine("AliveTimer");

    }


    /*随机激活怪物*/
    private void ActivateMonster()
    {
        int index = Random.Range(0, monsters.Length);
        activeMonster = monsters[index];
        activeMonster.SetActive(true);
        activeMonster.GetComponent<BoxCollider>().enabled = true;

        //调用死亡时间协程
        StartCoroutine("DeathTimer");
    }

    /*迭代器：设置生成怪物等待时间*/
    IEnumerator AliveTimer()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        ActivateMonster();
    }

    /*将激活状态的怪物设为未激活状态*/
    private void DeActivateMonster()
    {
        if (activeMonster != null)
        {
            activeMonster.GetComponent<BoxCollider>().enabled = false;
            activeMonster.SetActive(false);
            activeMonster = null;
        }

        //调用激活时间协程，达到反复激活和死亡的循环
        StartCoroutine("AliveTimer");
    }

    /*迭代器：设置怪物死亡等待时间*/
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(Random.Range(3, 8));
        DeActivateMonster();
    }

    /*更新生命周期，当子弹击中怪物时，或开启新游戏时
       停止所有协程
       将当前处于激活状态的怪物变为未激活状态，清空activeMonster
       重新开始AliveTimer协程(随机激活怪物)
   */
    public void UpdateMonster()
    {
        StopAllCoroutines();

        if (activeMonster != null)
        {
            activeMonster.GetComponent<BoxCollider>().enabled = false;
            activeMonster.SetActive(false);
            activeMonster = null;
        }

        StartCoroutine("AliveTimer");
    }

    /*按照给定怪物类型激活怪物*/
    public void ActivateMonsterByType(int type)
    {
        StopAllCoroutines();

        if (activeMonster != null)
        {
            activeMonster.GetComponent<BoxCollider>().enabled = false;
            activeMonster.SetActive(false);
            activeMonster = null;
        }

        activeMonster = monsters[type];
        activeMonster.SetActive(true);
        activeMonster.GetComponent<BoxCollider>().enabled =true;

        StartCoroutine("DeathTimer");
    }


    /*游戏结束时将激活状态的怪物设为未激活状态*/
    public void GameOver()
    {
        if (activeMonster != null)
        {
            activeMonster.GetComponent<BoxCollider>().enabled = false;
            activeMonster.SetActive(false);
            activeMonster = null;
        }
    }
}
