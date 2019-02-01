using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_MoveTo : MonoBehaviour
{
    private GameObject goldCollection;

     void Start()
    {
        goldCollection = GameObject.Find("GoldCollection");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,goldCollection.transform.position,10*Time.deltaTime);
    }
}
