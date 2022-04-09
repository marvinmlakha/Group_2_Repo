using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    [SerializeField] PlayerMovement player;
    Animator myAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;

    float leftX = -0.231f;
    float leftY = -0.391f;
    float rightX = 0.402f;
    float rightY = -0.382f;

    float x;
    float y;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();

        x = leftX;
        y = leftY;
    }

    void Update()
    {
        transform.position = thingToFollow.transform.position + new Vector3 (x,y,0);
        CheckWeaponState();
    }

    void CheckWeaponState(){
        
        if(player.GetHand()){
            spriteRenderer.enabled = true;
        }
        else{
            spriteRenderer.enabled = false;
        }
    }

    
}
