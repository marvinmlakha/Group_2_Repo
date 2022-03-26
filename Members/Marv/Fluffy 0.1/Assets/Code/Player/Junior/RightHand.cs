using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    [SerializeField] PlayerMovement playerMovement;
    Animator myAnimator;

    bool playerHasHorizontalSpeed;
    
    float leftX = -0.231f;
    float leftY = -0.391f;
    float rightX = 0.402f;
    float rightY = -0.382f;

    float x;
    float y;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHasHorizontalSpeed = playerMovement.getplayerHasHorizontalSpeed();

        myAnimator = GetComponent<Animator>();
        
        x = rightX;
        y = rightY;
    }

    void Update()
    {
        transform.position = thingToFollow.transform.position + new Vector3 (x,y,0);
        // Run();
        // FlipSprite();
    }

    // void Run()
    // {
    //     playerHasHorizontalSpeed = playerMovement.getplayerHasHorizontalSpeed();

    //     myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    // }

    // void FlipSprite()
    // {
    //     playerHasHorizontalSpeed = playerMovement.getplayerHasHorizontalSpeed();

    //     if (playerHasHorizontalSpeed)
    //     {
    //         x = leftX - .1f;
    //         y = leftY;
    //     }
    //     else{
    //         x = rightX;
    //         y = rightY;
    //     }
    // }
}
