using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyMovement : MonoBehaviour
{
    //float startY = 1050f;
    float EndY = 1120f;
    float moveSpeed = 40f;
    Vector3 posChange;
    bool moveIsOn = false;

    void Start()
    {
        //transform.position = new Vector3(-11, startY, 0);
        Invoke ("startMove", 3);
    }

    void startMove()
    {
        moveIsOn = true;
    }

    void Update()
    {
        if (moveIsOn)
        {
            Move();
        }
    }

    void Move()
    {
        //Debug.Log(transform.position);
        if (transform.position.y > EndY)
        {
            //Debug.Log("Not at EndPos yet");
            //Debug.Log("Old position: " + transform.localScale);
            posChange = new Vector3(0, moveSpeed, 0); //* Time.deltaTime;
            //Debug.Log(posChange);
            transform.position -= posChange;
            //Debug.Log("New position: " + transform.localScale);
        }
    }
}
