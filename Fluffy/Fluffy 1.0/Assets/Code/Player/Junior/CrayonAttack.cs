using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrayonAttack : MonoBehaviour
{
    
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawn;

    [SerializeField] CrayonHand crayonHand;

    int waitBetweenShots = 50;
    bool canShoot;

    AudioManager music;

    void Start()
    {
        crayonHand = FindObjectOfType<CrayonHand>();
        music = FindObjectOfType<AudioManager>();
    }

    void Update(){
        waitBetweenShots -= 1;
        if(waitBetweenShots <= 0){
            waitBetweenShots = 0;
            canShoot = true;
        }
    }

    public void Attack(){
        if(canShoot){
            crayonHand.SetAttackAnimation();
            
            music.Play("Shoot");

            Instantiate(bullet, spawn.position, spawn.rotation);

            waitBetweenShots = 50;
            canShoot = false;
        }
    }
}
