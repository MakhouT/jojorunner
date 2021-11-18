
using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sound;
    public bool muted = false;

    public static AudioManager instance;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sound)
        {
           s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;


            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }
        
    }

    public void Play(string name)
    {
    if(muted == false)
        {
            Sound s = Array.Find(sound, sound => sound.name == name);
            s.source.Play();
        }
     
    }
    public void Mute()
    {
        if (muted)
        {
            foreach(Sound s in sound)
            {
                s.source.GetComponent<AudioSource>().mute = false;
            }
            muted = false;
        }
        else
        {
            foreach (Sound s in sound)
            {
                s.source.GetComponent<AudioSource>().mute = true;
            }
            muted = true;
        }
    }
}

