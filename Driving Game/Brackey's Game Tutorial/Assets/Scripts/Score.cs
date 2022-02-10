using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player; // Refrerence to the tranform of the player object
    public Text scoreText; // Reference to the score text on the canvas


    // Update is called once per frame
    void Update()
    {
        // Setting the scoreText equal to the z position of the player transform
        // as the game updates and the distance travleed in the z direction increases
        scoreText.text = player.position.z.ToString("0");  
       
    }
}
