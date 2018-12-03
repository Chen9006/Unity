using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinController : MonoBehaviour
{
    private bool isFly = false;
    private bool isReach = false;
    private Transform startPoint;
    public float speed = 15f;

    private Transform circle;

    private Vector3 targetPosition; //距离圆周的向量


    // Use this for initialization
    void Start()
    {
        startPoint = GameObject.Find("StartPoint").transform;
        circle = GameObject.Find("Circle").transform;

        targetPosition.y = circle.position.y - 1.541f;
    }

    // Update is called once per frame
    void Update()
    {
        /*针就位*/
        if (isFly == false)
        {
            if (isReach == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, startPoint.position) < 0.05f)
                {
                    isReach = true;
                }
            }
        }

        else
        {
            /*针朝圆发射*/
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;

                transform.parent = circle;

                isFly = false;
            }
        }
    }

    public void StartFly()
    {
        isFly = true;
        isReach = true;
    }

}
