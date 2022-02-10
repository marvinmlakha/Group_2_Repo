using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the tranform of the player object
    public Vector3 offset; // Creating a 3D vector to be the offset postion of the camera

    // Update is called once per frame
    void Update()
    {
        // Postion of the camera is being set to the player postion plus the offset vector
        transform.position = player.position + offset;
    }
}
