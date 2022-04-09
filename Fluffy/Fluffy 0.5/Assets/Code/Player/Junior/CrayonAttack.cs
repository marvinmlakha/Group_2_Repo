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

    void Start()
    {
        crayonHand = FindObjectOfType<CrayonHand>();
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
            Instantiate(bullet, spawn.position, spawn.rotation);

            waitBetweenShots = 50;
            canShoot = false;
        }
    }
}
