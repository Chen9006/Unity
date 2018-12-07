using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Animation anim;

    /*怪物的两种动画状态*/
    public AnimationClip idleClip;
    public AnimationClip dieClip;

    public AudioSource kickAudio;//撞击音效

    public int monsterType;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
        anim.clip = idleClip;
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        /*当检测碰撞的物体是子弹的话，销毁子弹*/
        if (collision.collider.tag=="Bullet")
        {
            Destroy(collision.collider.gameObject);

            //播放撞击音效
            kickAudio.Play();

            /*改为死亡状态并播放动画*/
            anim.clip = dieClip;
            anim.Play();
            gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine("Deactivate");

            ////更新分数
            UIManager._instance.AddScore();
        }
    }


    /*当怪物为disable状态时，将默认动改成idle动画*/
    private void OnDisable()
    {
        anim.clip = idleClip;
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.3f);
        //使当前怪物变为未激活状态，并使整个循环重新开始

        gameObject.GetComponentInParent<TargetManager>().UpdateMonster();
    }


}
