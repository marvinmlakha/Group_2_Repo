using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement; // A reference to our PlayerMovement script
   

    // This function runs when we hit another object.
    // We get information about the collsion using the parameters and calling it "collisionInfo".
    private void OnCollisionEnter(Collision collisionInfo)
    {
        // We check if the object we collied with has a tag called "Obstacle". 
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false; // Disable the players movement
            FindObjectOfType<GameManager>().EndGame();
        }

    }

}
