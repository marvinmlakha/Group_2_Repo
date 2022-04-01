using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBulletCollider;
    [SerializeField] PlayerMovement player;

    int damage = 2;

    float movementSpeed = 5f;
    float moveDirection;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myBulletCollider = GetComponent<CapsuleCollider2D>();
        moveDirection = player.transform.localScale.x * movementSpeed;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(moveDirection, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "enemy"){
            other.GetComponent<EnemyScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.tag == "Player"){
        }
        else if(other.tag == "bullet"){
        }
        else{
            Destroy(gameObject);
        }
    }
}
