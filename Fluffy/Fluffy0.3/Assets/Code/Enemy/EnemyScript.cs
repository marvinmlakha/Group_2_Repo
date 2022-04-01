// Enemy script version 0.1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript: MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Collider2D myCapsuleCollider;
    Animator myAnimator;

    // Note: When an enemy is created, moveSpeed and attackSpeed should be positive numbers, 
    // and knockbackSpeed should be negative. The enemy sprite should face right.
    [Header("Movement Settings")]
    [Tooltip("Normal moving speed")]
    [SerializeField] float moveSpeed = 1f;
    [Tooltip("Speed when charging at Junior")]
    [SerializeField] float attackSpeed = 5f;
    [Tooltip("Backwards speed after colliding with Junior")]
    [SerializeField] float knockbackSpeed = -7f;
    [Tooltip("How far backwards the enemy will move after colliding with Junior")]
    [SerializeField] float knockbackDistance = 4f;
    [Tooltip("When in the 'moving' state, the distance from " +
            "either side of the enemy's starting point on " +
            "the x-axis that it can move before turning around")]
    [SerializeField] float xAxisMovementRange = 10f;

    [Header("Attack Settings")]
    [Tooltip("This should eventually be checked for every enemy, but can be unchecked for testing purposes")]
    [SerializeField] bool canDealDamage = false;
    [Tooltip("This should eventually be checked for every enemy, but can be unchecked for testing purposes")]
    [SerializeField] bool canTakeDamage = true;
    [Tooltip("Enemy's health when it is created")]
    [SerializeField] int health = 12;
    [Tooltip("The number of half-hearts Junior will lose when attacked by this enemy")]
    [SerializeField] int attack = 1;
    [Tooltip("(Only for non-flying enemies) The maximum y-axis difference between the " + 
            "enemy and Junior for the enemy to attack him")]
    [SerializeField] float maxYAxisDifference = 0.2f;
    [Tooltip("After taking damage, the distance the enemy has to get from Junior before it can take or give damage again. " +
            "If this value is too small, the enemy will die after one collision. " +
            "Will be a higher number for larger enemies.")]
    [SerializeField] float safeZone = 3f;

    [Header("Flying Settings")]
    [Tooltip("Check this box if the enemy can fly")]
    [SerializeField] bool flying = false;
    [Tooltip("(Only for flying enemies) The speed at which the enemy will return to its " +
            "starting point on the y-axis when it is not attacking or retreating")]
    [SerializeField] float returnToStartSpeed = 7f;

    [Header("Other Settings")]
    [Tooltip("A higher number means that the enemy can see Junior from farther away")]
    [SerializeField] float vision = 3f;
    [Tooltip("The direction the enemy starts moving when it is created (Can equal 'right' or 'left')")]
    [SerializeField] string startDirection = "right";
    
    bool damageLocked = false; // For keeping track of when the enemy should be able to lose health
    float distanceFromPlayer; // Distance from the player
    string direction; // Can equal "right" or "left"
    string state; // Can equal "moving" "attacking" "returning" "retreating" or "dying"
    float yStart; // Used by flying enemies to return to their starting Y point
    float xStart; // Used by the Move function (along with the xAxisMovementRange variable)
                  // to determine when the enemy should change directions
    GameObject player;
    GameObject healthbar;

    void Start()
    {   
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();

        player = GameObject.Find("Junior");
        healthbar = GameObject.Find("player_placeholder");

        direction = "right";
        if (startDirection == "left")
        {
            ChangeDirection();
        }
        state = "moving";
        myAnimator.Play("Moving");
        //Debug.Log("Moving");

        yStart = transform.position.y;
        xStart = transform.position.x;
    }

    void Update()
    {

        if (state == "dying")
        {
            myRigidbody.velocity = Vector3.zero; // Stop movement
            myAnimator.Play("Dying");
            return;
        }

        if (IsOutOfBounds()) // If the enemy is outside of the boundaries specified by xAxisMovementRange
        {
            // Debug.Log("Out of bounds");
            ChangeDirection();
        }

        distanceFromPlayer = GetDistanceToPlayer();
        //Debug.Log(distanceFromPlayer);
        Vector2 playerLoc = player.transform.position;
        Vector2 enemyLoc = transform.position;

        if(distanceFromPlayer > safeZone) // After being attacked, the enemy or player can't take more damage until Junior gets farther away.
                                        // This is to stop either one from losing too much health from one collision.
        {
            //Debug.Log("damageLocked = false");
            damageLocked = false;
        }
        if (state == "retreating")
        {
            Retreat();
        }
        else if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) // Collision with Junior
        {
            if (state == "attacking" && canDealDamage && !damageLocked)
            {
                healthbar.GetComponent<PlayerHearts>().TakeDamage(attack);
                damageLocked = true; // Prevent more player health loss from this collision
            }              
           
            if (state != "attacking" && canTakeDamage && !damageLocked)
            {
                TakeDamage(1);
                damageLocked = true; // Prevent more enemy health loss from this collision
            }

            if (IsFacingPlayer(playerLoc, enemyLoc))
            {
                state = "retreating";
                myAnimator.Play("Retreating");
                //Debug.Log("Retreating");
                Retreat();
            }
            
        }
        else if (state != "attacking" && flying && (transform.position.y < yStart - 1 || transform.position.y > yStart + 1))
        // If the enemy is flying and not in the middle of attacking or retreating, it should
        // return to its starting point on the y axis (It can be off by 1, to prevent a glitch)
        {
            //Debug.Log("Returning to yStart");
            state = "returning";
            myAnimator.Play("Returning");
            Return();
        }
        else if (distanceFromPlayer <= vision && ((IsFacingPlayer(playerLoc, enemyLoc) || state == "attacking"))) 
        // Junior is within attacking distance, and the enemy is either facing him or already attacking
        {
            float playerY = player.transform.position.y;
            float enemyY = transform.position.y;
            float playerX = player.transform.position.x;
            float enemyX = transform.position.x;

            if (flying == false) // If the enemy can't fly, it must check to see if Junior 
                                // is close on the y-axis before attacking
            {
                
                if (Mathf.Abs(playerY - enemyY) <= maxYAxisDifference)
                {
                    state = "attacking";
                    myAnimator.Play("Attacking");
                    //Debug.Log("Attacking");
                    Attack();
                }
            }
            else // If the enemy can fly, Junior just has to be lower on the y-axis in order to attack him
            {
                if (playerY < enemyY)
                {
                    state = "attacking";
                    myAnimator.Play("Attacking");
                    //Debug.Log("Attacking");
                    Attack();
                }
            }
        }
        else // If the enemy doesn't retreat, return or attack, it will move normally
        {
            state = "moving";
            myAnimator.Play("Moving");
            //Debug.Log("Moving");
            Move();
        }
    }

    // STATE METHODS

    void Move()
    {
        // If the enemy has reached the end of their movement range, change direction before moving
        if (IsOutOfBounds())  
        {
            ChangeDirection();
        }

        myRigidbody.velocity = new Vector2(moveSpeed, 0f); // Move normally
    }

    void Return()
    {
        if (transform.position.y < yStart)
            {
                myRigidbody.velocity = new Vector2(0, Mathf.Abs(returnToStartSpeed));
            }
        else if (transform.position.y > yStart)
            {
                myRigidbody.velocity = new Vector2(0, -(Mathf.Abs(returnToStartSpeed)));
            }
    }

    void Attack()
    {
        // Move towards Junior
        if (flying)
        {
            myRigidbody.velocity = new Vector2(attackSpeed, -(Mathf.Abs(attackSpeed))); // Diagonal movement
        }
        else
        {
            myRigidbody.velocity = new Vector2(attackSpeed, 0f); // Horizontal movement
        }
        
    }

    void Retreat()
    {
        Vector2 playerLoc = player.transform.position;
        Vector2 enemyLoc = transform.position;

        if (distanceFromPlayer < knockbackDistance && IsFacingPlayer(playerLoc, enemyLoc)) // If the enemy is being knocked back
        {
            if (flying)
            {
                myRigidbody.velocity = new Vector2(knockbackSpeed, Mathf.Abs(knockbackSpeed));
            }
            else
            {
                myRigidbody.velocity = new Vector2(knockbackSpeed, 0f);
            }
        }
        else
        {
            state = "moving";
            myAnimator.Play("Moving");
            //Debug.Log("Moving");
        }
    }

    void Die() 
    {
        Destroy(gameObject);
    }

    // SETTERS/GETTERS

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public int GetHealth()
    {
        return (health);
    }

    // If Junior collides with the enemy, these two methods will tell the player
    // whether the enemy is attacking and how much damage it will do to him
    public string GetState()
    {
        return (state);
    }

    public int GetAttack()
    {
        return (attack);
    }

    // OTHER METHODS

    bool IsOutOfBounds()
    {
        return ((direction == "left" && transform.position.x - xStart < -xAxisMovementRange) || 
                (direction == "right" && transform.position.x - xStart > xAxisMovementRange)); 
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (flying)
        {
            if (other.gameObject.name == "Tilemap")
            {
                if (state != "retreating")
                {
                    ChangeDirection();
                }
                state = "returning";
                myAnimator.Play("Returning");
                Return();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    { 
        if (!flying)
        {
            if (other.gameObject.name == "Tilemap")
            {
                if (state != "retreating")
                {
                    ChangeDirection();
                }
                else
                {
                    state = "moving";
                    myAnimator.Play("Moving");
                    //Debug.Log("Moving");
                    Move();
                }
            }
        }
    }

    void ChangeDirection()
    {
        myRigidbody.velocity = Vector3.zero; // Stop moving before changing direction
                                            // (Added to stop enemy from going through walls)
        moveSpeed = -moveSpeed;
        attackSpeed = -attackSpeed;
        knockbackSpeed = -knockbackSpeed;
        FlipEnemyFacing();

        if (direction == "right")
        {
            direction = "left";
        }
        else
        {
            direction = "right";
        }
        //Debug.Log("Changing direction to " + direction);
        myRigidbody.velocity = new Vector2(moveSpeed, 0f); // Continue moving at normal speed 
    }

    void FlipEnemyFacing()
    {
        Vector3 objectScale = transform.localScale;
        transform.localScale = new Vector3(-objectScale.x, objectScale.y, objectScale.z);
    }

    float GetDistanceToPlayer()
    {
        Vector2 playerLoc = player.transform.position;
        Vector2 enemyLoc = transform.position;
        var dist = Vector2.Distance(playerLoc, enemyLoc);
        return (dist);
    }

    bool IsFacingPlayer(Vector2 playerLoc, Vector2 enemyLoc)
    {
        return ((playerLoc.x < enemyLoc.x && direction == "left")
             || (playerLoc.x > enemyLoc.x && direction == "right"));
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Hit for" + amount);
        if(health <= 0)
        {
            Die();
        }
    }
}