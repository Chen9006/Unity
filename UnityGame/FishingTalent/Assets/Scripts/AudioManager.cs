using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager _instance;

    private bool isMute = false;

    public AudioSource bgmAudio;

    public AudioClip seaWaveClip;
    public AudioClip GoldClip;
    public AudioClip rewardClip;
    public AudioClip FireClip;
    public AudioClip changeGunClip;
    public AudioClip lvUpClip;

    public bool IsMute
    {
        get
        {
            return isMute;
        }
    }

    private void Awake()
    {
        _instance = this;
        isMute = (PlayerPrefs.GetInt("mute") == 0) ? false : true;
        doMute();
    }

    public void switchMuteState(bool isOn)
    {
        isMute = !isOn;
        doMute();
    }

    public void playEffectAudio(AudioClip clip)
    {
        if (!isMute)
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }
    }

    public void doMute()
    {
        if (isMute)
        {
            bgmAudio.Pause();
        }
        else
        {
            bgmAudio.Play();
        }
    }

}
