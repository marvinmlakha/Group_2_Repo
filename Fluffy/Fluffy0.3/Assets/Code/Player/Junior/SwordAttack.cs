using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    [SerializeField] SwordHand swordHand;
    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Transform attackPoint;
    public float attackRange = 0.5f;
    public int damage = 4;

    void Start()
    {
        swordHand = FindObjectOfType<SwordHand>();
    }
    
    public void Attack(){
        swordHand.SetAttackAnimation();

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
