using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //[SerializeField] float bulletSpeed = 5f;
    Rigidbody2D myRigidbody;
    //PlayerMovementScript player;
    public Vector2 speed;
    GameObject healthbar;
    public int damage;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        healthbar = GameObject.Find("player_placeholder");
        //player = FindObjectOfType<PlayerMovementScript>();
        //xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody.velocity = speed;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            healthbar.GetComponent<PlayerHearts>().TakeDamage(damage);
        }
        if(other.gameObject.tag != "enemy")
        {
            //Debug.Log("Destroyed in OnCollisionEnter2D by " + other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
