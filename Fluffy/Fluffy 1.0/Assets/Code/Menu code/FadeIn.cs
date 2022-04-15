using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] float startAlpha = 0f;
    [SerializeField] float endAlpha = 255f;
    [SerializeField] float fadeSpeed = 0.01f;
    [SerializeField] int delay = 1;
    bool fadeIsOn = false;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = startAlpha;
        image.color = c;
        Invoke ("startFade", delay);
    }

    void startFade()
    {
        fadeIsOn = true;
    }

    void Update()
    {
        if (fadeIsOn)
        {
            Fade();
        }
    }

    void Fade()
    {
        image = GetComponent<Image>();
        Color c = image.color;
        if (image.color.a < fadeSpeed)
        {
            if (gameObject.tag == "fader")
            {
                gameObject.active = false;
            }
        }
        
        if (image.color.a < endAlpha)
        {
            //Debug.Log("Not at EndScale yet");
            //Debug.Log("Old scale: " + transform.localScale);
            
            c.a += fadeSpeed;
            image.color = c;
            //Debug.Log("New scale: " + transform.localScale);
        }
        else if (image.color.a > endAlpha)
        {
            //Debug.Log("image.color a > endAlpha");
            c.a -= fadeSpeed;
            //Debug.Log("c.a: " + c.a + " fadeSpeed: " + fadeSpeed);
            image.color = c;
            //Debug.Log(c);
        }
    }
}
