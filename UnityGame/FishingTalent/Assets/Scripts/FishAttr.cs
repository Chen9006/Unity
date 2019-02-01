using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttr : MonoBehaviour
{

    public int EXP;
    public int Gold;
    public int HP;
    public int maxNum;
    public int maxSpeed;

    public GameObject diePrefabs;

    public GameObject goldPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }

    void takeDamage(int value)
    {
        //伤害
        HP = HP - value;
        if (HP <= 0)
        {
            GameObject die = Instantiate(diePrefabs);
            die.transform.SetParent(gameObject.transform.parent, false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;

            GameController._instance.gold += Gold;
            GameController._instance.EXP += EXP;

            GameObject gold_instance = Instantiate(goldPrefab);
            gold_instance.transform.SetParent(gameObject.transform.parent, false);
            gold_instance.transform.position = transform.position;
            gold_instance.transform.rotation = transform.rotation;

            if (gameObject.GetComponent<Ef_PlayEffect>() != null)
            {
                AudioManager._instance.playEffectAudio(AudioManager._instance.rewardClip);
                gameObject.GetComponent<Ef_PlayEffect>().PlayEffect();
            }

            Destroy(gameObject);
        }
    }
}
