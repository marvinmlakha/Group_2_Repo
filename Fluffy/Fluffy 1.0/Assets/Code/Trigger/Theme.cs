using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Theme : MonoBehaviour
{
    
    public static Theme instance;

    public Sound[] theme;

    private void Awake() {

        if(instance == null){
            
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(instance);

        foreach (Sound s in theme)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PlayTheme("Theme");
    }

    void PlayTheme (string name){
        Sound s = Array.Find(theme, theme => theme.name == name);
        s.source.Play();
    }
}
