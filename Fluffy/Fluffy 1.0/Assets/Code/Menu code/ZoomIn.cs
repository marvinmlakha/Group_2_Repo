using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    [SerializeField] float startScale = 0f;
    [SerializeField] float EndScale = 1f;
    [SerializeField] float zoomSpeed = 1f;
    [SerializeField] int delayTime;
    [SerializeField] bool instantZoom = false;
    Vector3 scaleChange;
    bool zoomIsOn = false;

    void Start()
    {
        transform.localScale = new Vector3(startScale, startScale, startScale);
        Invoke ("startZoom", delayTime);
    }

    void startZoom()
    {
        zoomIsOn = true;
    }

    void Update()
    {
        if (zoomIsOn)
        {
            if (!instantZoom)
                {
                    Zoom();
                }
            else
            {
                instZoom();
            }
        }
    }

    void Zoom()
    {
        if (transform.localScale.x < EndScale)
        {
            //Debug.Log("Not at EndScale yet");
            //Debug.Log("Old scale: " + transform.localScale);
            scaleChange = new Vector3(zoomSpeed, zoomSpeed, zoomSpeed) * Time.deltaTime;
            transform.localScale += scaleChange;
            //Debug.Log("New scale: " + transform.localScale);
        }
    }

    void instZoom()
    {
        if (transform.localScale.x != EndScale)
        {
            //Debug.Log("Not at EndScale yet");
            //Debug.Log("Old scale: " + transform.localScale);
            scaleChange = new Vector3(EndScale, EndScale, EndScale);
            transform.localScale = scaleChange;
            //Debug.Log("New scale: " + transform.localScale);
        }
    }
}
