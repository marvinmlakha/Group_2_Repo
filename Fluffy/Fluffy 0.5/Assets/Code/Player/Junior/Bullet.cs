using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBulletCollider;
    [SerializeField] PlayerMovement player;

    int damage = 3;
    int rotation = 0;

    float movementSpeed = 7.5f;
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

        rotation -= 5;

        transform.Rotate (new Vector3 (0, 0, rotation) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "enemy"){
            if (other.GetComponent<EnemyScript>().GetState() != "dying")
            {
                other.GetComponent<EnemyScript>().CollideWithWeapon(damage);
            }
            Destroy(gameObject);
        }
        else if(other.tag == "Player" || other.tag == "bullet" || other.tag == "hole" ||
                other.tag == "respawn" || other.tag == "sword" || other.tag == "staff" ||
                other.tag == "crayon" || other.tag == "star"){
        } 
        else{
            Destroy(gameObject);
        }
    }
}
