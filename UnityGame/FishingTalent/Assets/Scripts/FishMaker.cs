using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour
{
    public Transform[] genPositions;
    public GameObject[] fishPrefabs;
    public Transform fishHolder;

    public float waitGenWaitTime = 0.3f;
    public float fishGenWaitTime = 0.5f;

    void Start()
    {
        InvokeRepeating("MakeFishes",0,waitGenWaitTime);
    }

    void MakeFishes()
    {
        int genPosIndex = Random.Range(0, genPositions.Length);
        int fishPreIndex = Random.Range(0, fishPrefabs.Length);

        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxNum;
        int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxSpeed;

        int num = Random.Range((maxNum / 2) + 1, maxNum);
        int speed = Random.Range(maxSpeed/ 2, maxSpeed);

        int moveType = Random.Range(0, 2);// 0 直走 1 转弯
        int angleOffset;                                  //直走倾斜角
        int angleSpeed;                                 //转弯角速度

        switch (moveType)
        {
            case 0:  //直走鱼群生成
                angleOffset = Random.Range(-22, 22);
                GenStraightFish(genPosIndex,fishPreIndex,num,speed, angleOffset);
                break;

            case 1:  //转弯鱼群生成
                if(Random.Range(0,2)==0)
                {
                    angleSpeed = Random.Range(-15, -9);
                }
                else
                {
                    angleSpeed = Random.Range(9, 15);
                }

                StartCoroutine(GenTurnFish(genPosIndex, fishPreIndex,num,speed,angleSpeed));
                break;

            default:
                break;
        }
    }

    IEnumerator GenStraightFish(int genPosIndex, int fishPreIndex, int num, int speed, int angleOffset)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish= Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = genPositions[genPosIndex].localPosition;
            fish.transform.localRotation = genPositions[genPosIndex].localRotation;
            fish.transform.Rotate(0, 0, angleOffset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed=speed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }

    IEnumerator GenTurnFish(int genPosIndex, int fishPreIndex, int num, int speed, int angleSpeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = genPositions[genPosIndex].localPosition;
            fish.transform.localRotation = genPositions[genPosIndex].localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            fish.AddComponent<Ef_AutoRotate>().speed = angleSpeed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }
}
