using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMPFadeIn : MonoBehaviour
{
    [SerializeField] byte startAlpha = 0;
    [SerializeField] float endAlpha = 255f;
    [SerializeField] float fadeSpeed = 0.01f;
    [SerializeField] int delay = 1;
    bool fadeIsOn = false;
    TextMeshProUGUI image;

    void Start()
    {
        image = GetComponent<TextMeshProUGUI>();
        Color32 c = image.faceColor;
        c.a = startAlpha;
        image.faceColor = c;
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
        if (image.faceColor.a < endAlpha)
        {
            //Debug.Log("Not at EndScale yet");
            //Debug.Log("Old scale: " + transform.localScale);
            image = GetComponent<TextMeshProUGUI>();
            Color c = image.faceColor;
            c.a += fadeSpeed;
            image.faceColor = c;
            //Debug.Log("New scale: " + transform.localScale);
        }
    }
}
