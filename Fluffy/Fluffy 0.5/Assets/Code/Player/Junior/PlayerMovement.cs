using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    bool swordAppear;
    static bool swordPickedUp = false;
    bool staffAppear;
    static bool staffPickedUp = false;
    bool crayonAppear;
    static bool crayonPickedUp = false;
    bool handAppear;
    bool signTouched;
    static bool doubleJump;

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

        swordAppear = false;
        staffAppear = false;
        crayonAppear = false;
        handAppear = true;

        doubleJump= false;

        startPosition = transform.position;
        signTouched = false;
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    public bool GetSword(){
        return swordAppear;
    }

    public bool GetStaff(){
        return staffAppear;
    }

    public bool GetCrayon(){
        return crayonAppear;
    }

    public bool GetHand(){
        return handAppear;
    } 

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    
    void OnJump(InputValue value)
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))){
            grounded = true;
        }

        if(value.isPressed){
            if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))){
                myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
            }
            else{
                if(doubleJump){
                    if(grounded){
                        double jumpSpeedDouble = jumpSpeed*.8;
                        int jumpSpeedReduced = (int)jumpSpeedDouble;
                        
                        Vector2 playerVelocity = new Vector2(0, 0);
                        myRigidbody.velocity = playerVelocity;

                        myRigidbody.velocity += new Vector2 (0f, jumpSpeedReduced);
                        grounded = false;
                    }
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
        if(swordPickedUp){
            swordAppear = true;
            staffAppear = false;
            crayonAppear = false;
            handAppear = false;
        }
    }

    void OnWeapon_2(){
        if(staffPickedUp){
            swordAppear = false;
            staffAppear = true;
            crayonAppear = false;
            handAppear = false;
        }
    }

    void OnWeapon_3(){
        if(crayonPickedUp){
            swordAppear = false;
            staffAppear = false;
            crayonAppear = true;
            handAppear = false;
        }
    }

    void OnWeapon_4(){
        swordAppear = false;
        staffAppear = false;
        crayonAppear = false;
        handAppear = true;
    }

    void OnWeapon_5(){
        if(swordAppear){
            swordAttack.Attack();
        }
        else if(staffAppear){
            staffAttack.Attack();
        }
        else if(crayonAppear){
            crayonAttack.Attack();
        }
    }

    void returnToSign(){
        Vector2 playerVelocity = new Vector2(0, 0);
        myRigidbody.velocity = playerVelocity;

        transform.position = respawnPosition;
        hearts.TakeDamage(1);
    }

    void returnToStart(){
        Vector2 playerVelocity = new Vector2(0, 0);
        myRigidbody.velocity = playerVelocity;

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
            swordPickedUp = true;
            Destroy(other.gameObject);
        }
        else if(other.tag == "staff"){
            staffPickedUp = true;
            Destroy(other.gameObject);
        }
        else if(other.tag == "crayon"){
            crayonPickedUp = true;
            Destroy(other.gameObject);
        }
        else if(other.tag == "star"){
            Destroy(other.gameObject);
        }
        else if(other.tag == "heart"){
            Destroy(other.gameObject);
            hearts.Heal(2);
        }
        else if(other.tag == "heart"){
            Destroy(other.gameObject);
            hearts.Heal(2);
        }
        else if(other.tag == "doubleJump"){
            Destroy(other.gameObject);
            doubleJump = true;
        }
        else if(other.tag == "bannerEnd"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(other.tag == "wall"){
            Die();
        }
    }

    public void KnockBack(){
        Vector2 playerVelocity = new Vector2(0, 0);
        myRigidbody.velocity = playerVelocity;

        myRigidbody.velocity += new Vector2 (0f, jumpSpeed * .5f);
        grounded = false;
    }

    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // SceneManager.LoadScene(0);
    }

}
