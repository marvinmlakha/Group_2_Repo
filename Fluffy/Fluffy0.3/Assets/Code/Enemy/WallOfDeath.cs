using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    
    [SerializeField] float wallSpeed = .03f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(wallSpeed, 0, 0);
    }

}