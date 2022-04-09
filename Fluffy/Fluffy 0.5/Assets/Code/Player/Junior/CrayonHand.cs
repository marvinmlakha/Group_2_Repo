using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrayonHand : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    Animator myAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;
    }

    
    void Update()
    {
        CheckWeaponState();
    }

    void CheckWeaponState(){
        
        if(player.GetCrayon()){
            spriteRenderer.enabled = true;
        }
        else{
            spriteRenderer.enabled = false;
        }
    }

    public void SetAttackAnimation(){
        myAnimator.SetTrigger("onAttack");
    }
}
