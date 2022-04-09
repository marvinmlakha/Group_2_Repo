using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluffy : MonoBehaviour
{
    BoxCollider2D myFluffyCollider;
    PlayerMovement playerMovement;
    Vector3 position;
    Vector3 newPosition;

    int waitBetweenShake = 20;

    float shake;


    void Start()
    {
        myFluffyCollider = GetComponent<BoxCollider2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        shake = 0.05f;
    }

    void Update()
    {  
        waitBetweenShake -=1; 
        position = playerMovement.transform.position + new Vector3 (-1,2);

        if(waitBetweenShake <= 0){
            newPosition = UnityEngine.Random.insideUnitSphere * shake;
            transform.position = position + newPosition;
            waitBetweenShake = 25;
        }
        else{
            transform.position = position + newPosition;
        }
    }
}
