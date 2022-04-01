using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 7.5f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myFeetCollider;
    CapsuleCollider2D myPlayerCollider;

    [SerializeField] CrayonAttack crayonAttack;
    [SerializeField] SwordAttack swordAttack;
    [SerializeField] StaffAttack staffAttack;

    bool playerHasHorizontalSpeed;

    bool grounded;

    bool sword;
    bool staff;
    bool crayon;
    bool hand;
    bool signTouched;

     PlayerHearts hearts;

    [SerializeField] Vector3 respawnPosition;
    [SerializeField] Vector3 startPosition;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myPlayerCollider = GetComponent<CapsuleCollider2D>();

        swordAttack = FindObjectOfType<SwordAttack>();
        staffAttack = FindObjectOfType<StaffAttack>();
        crayonAttack = FindObjectOfType<CrayonAttack>();

        hearts = FindObjectOfType<PlayerHearts>();

        sword = false;
        staff = false;
        crayon = false;
        hand = true;

        startPosition = transform.position;
        signTouched = false;
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    public bool GetSword(){
        return sword;
    }

    public bool GetStaff(){
        return staff;
    }

    public bool GetCrayon(){
        return crayon;
    }

    public bool GetHand(){
        return hand;
    } 

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    
    void OnJump(InputValue value)
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            grounded = true;
        }

        if(value.isPressed){
            if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
                myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
            }
            else{
                if(grounded){
                    double jumpSpeedDouble = jumpSpeed*.8;
                    int jumpSpeedReduced = (int)jumpSpeedDouble;
                    myRigidbody.velocity += new Vector2 (0f, jumpSpeedReduced);
                    grounded = false;
                }
            }
            
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    public bool GetplayerHasHorizontalSpeed(){
        return playerHasHorizontalSpeed;
    }

    void OnWeapon_1(){
        sword = true;
        staff = false;
        crayon = false;
        hand = false;
    }

    void OnWeapon_2(){
        sword = false;
        staff = true;
        crayon = false;
        hand = false;
    }

    void OnWeapon_3(){
        sword = false;
        staff = false;
        crayon = true;
        hand = false;
    }

    void OnWeapon_4(){
        sword = false;
        staff = false;
        crayon = false;
        hand = true;
    }

    void OnAttackLeft(){
        
    }

    void OnAttackRight(){
        if(sword){
            swordAttack.Attack();
        }
        else if(staff){
            staffAttack.Attack();
        }
        else if(crayon){
            crayonAttack.Attack();
        }
    }

    void returnToSign(){
        transform.position = respawnPosition;
        hearts.TakeDamage(1);
    }

    void returnToStart(){
         transform.position = startPosition;
         hearts.TakeDamage(1);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "hole"){
            if(signTouched){
                returnToSign();
            }
            else{
                returnToStart();
            }
        }
        else if(other.tag == "respawn"){
            respawnPosition = other.transform.position + new Vector3 (0,2,0);
            signTouched = true;
        }
        else if(other.tag == "sword"){
            Destroy(other.gameObject);
        }
        else if(other.tag == "wall"){
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
