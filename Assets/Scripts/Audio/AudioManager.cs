using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct AudioClipEntry 
{
    public string name;
    public AudioClip clip;
};

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { private set; get; }

    AudioSource source;

    public AudioClipEntry[] globalAudioDictionary;

    void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Pause() => source.Pause();

    public void UnPause() => source.UnPause();

    public IEnumerator PlayAudioWithKey(string key) 
    {
        source.loop = false;
        foreach(AudioClipEntry entry in globalAudioDictionary)
        {
            if (entry.name == key) 
            {
                float playTime = entry.clip.length;
                source.clip = entry.clip;
                yield return new WaitForSeconds(playTime);
                break;
            }
        }

        // return to original background Music

        source.loop = true;
    }

}
