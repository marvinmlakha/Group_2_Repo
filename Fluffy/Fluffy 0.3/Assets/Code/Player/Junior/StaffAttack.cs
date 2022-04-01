using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttack : MonoBehaviour
{

    [SerializeField] Fire [] fire;

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Transform attackPoint;

    public float attackRange = 1f;
    public int damage = 2;

    public async void Attack(){
        for(int i = 0; i < fire.Length; i ++){
            fire[i].FireAnimation();
        }

        Collider2D [] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
    
        foreach (Collider2D enemy in enemiesHit)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected() {

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
