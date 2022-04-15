using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int backgroundSpeed = 200;
    Vector3 startPos;

    void Start() 
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < (startPos.x + 2560))
        {
            transform.Translate(backgroundSpeed * Time.deltaTime, 0, 0, Camera.main.transform);
        }
        else
        {
            //transform.position = new Vector3(-1280, 720, 0);
            transform.Translate(-2560, 0, 0, Camera.main.transform);
        }
    }
}
