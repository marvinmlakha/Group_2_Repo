using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttack : MonoBehaviour
{

    [SerializeField] Fire [] fire;

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Transform attackPoint;

    int waitBetweenShots = 50;
    bool canShoot;

    AudioManager music;

    public float attackRange = 1f;
    public int damage = 4;

    private void Start() {
        music = FindObjectOfType<AudioManager>();
    }

    void Update(){
        waitBetweenShots -= 1;
        if(waitBetweenShots <= 0){
            waitBetweenShots = 0;
            canShoot = true;
        }
    }

    public async void Attack(){
        if(canShoot){
            for(int i = 0; i < fire.Length; i ++){
                fire[i].FireAnimation();
            }

            music.Play("Explosion");

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
