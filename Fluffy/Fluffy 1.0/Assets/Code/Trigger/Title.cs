using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Title : MonoBehaviour
{
    
    public Sound[] theme;

    private void Awake() {
        foreach (Sound s in theme)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        PlayTheme("Theme");
    }
    
    void PlayTheme (string name){
        Sound s = Array.Find(theme, theme => theme.name == name);
        s.source.Play();
    }
}
