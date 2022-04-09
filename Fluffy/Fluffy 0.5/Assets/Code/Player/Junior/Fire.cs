using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Animator myAnimator;
    SpriteRenderer sprite;

    int timer = 40;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        sprite.enabled = false;
    }

    void Update()
    {
        timer--;
        if(timer<=0){
            timer = 0;
            sprite.enabled = false;
        }
    }

    public void FireAnimation(){
        timer = 25;
        sprite.enabled = true;
    }
}
