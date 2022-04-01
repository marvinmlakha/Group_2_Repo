using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [SerializeField] float moveSpeedX = .01f;
    [SerializeField] float moveSpeedY = .0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeedX, moveSpeedY, 0);
    }
}
