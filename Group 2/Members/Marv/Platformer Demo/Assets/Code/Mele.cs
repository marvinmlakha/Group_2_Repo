using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider other) {
        Debug.Log("Test");
        moveSpeed = -moveSpeed;
        Flip();
    }

    void Flip(){
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
}
