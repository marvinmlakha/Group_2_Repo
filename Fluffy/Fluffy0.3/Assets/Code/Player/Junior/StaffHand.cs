using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffHand : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;
    }

    
    void Update()
    {
        CheckWeaponState();
    }

    void CheckWeaponState(){
        
        if(player.GetStaff()){
            spriteRenderer.enabled = true;
        }
        else{
            spriteRenderer.enabled = false;
        }
    }
}
