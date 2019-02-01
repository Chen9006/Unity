using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollection : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gold")
        {
            AudioManager._instance.playEffectAudio(AudioManager._instance.GoldClip);
            Destroy(collision.gameObject);
        }
    }

}
