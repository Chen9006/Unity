using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHeadController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PinHead")
        {
            GameObject.Find("PinManager").GetComponent<PinManager>().GameOver();
        }
    }
}
