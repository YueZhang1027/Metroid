using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { private set; get; }

    AudioSource source;

    AudioClip bornClip;


    void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Pause() => source.Pause();

    public void UnPause() => source.UnPause();
}
