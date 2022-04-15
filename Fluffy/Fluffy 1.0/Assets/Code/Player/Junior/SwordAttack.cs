using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    [SerializeField] SwordHand swordHand;
    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Transform attackPoint;

    int waitBetweenShots = 50;
    bool canShoot;

    AudioManager music;

    public float attackRange = 0.5f;
    public int damage = 6;

    void Start()
    {
        swordHand = FindObjectOfType<SwordHand>();
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
            swordHand.SetAttackAnimation();

            music.Play("Sword");

            Collider2D [] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        
            foreach (Collider2D enemy in enemiesHit)
            {
                if (enemy.GetComponent<EnemyScript>().GetState() != "dying")
                {
                    enemy.GetComponent<EnemyScript>().CollideWithWeapon(damage);
                }
            }
        }
    }

    void OnDrawGizmosSelected() {

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
