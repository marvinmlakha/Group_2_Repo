using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrayonAttack : MonoBehaviour
{
    
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawn;

    [SerializeField] CrayonHand crayonHand;

    void Start()
    {
        crayonHand = FindObjectOfType<CrayonHand>();
    }

    public void Attack(){
        crayonHand.SetAttackAnimation();
        Instantiate(bullet, spawn.position, spawn.rotation);
    }
}
