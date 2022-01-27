using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    // Values for directional forces can always be changed in unity
    public float forwardForce = 1000; // Setting a value for the forward force
    public float sidewaysForce = 600f; // Setting a value for the sideways force
    


    // We marked this as "Fixed" Update because
    // we are using it to mess with physics.
    void FixedUpdate()
    {
        //Here we are adding a forward force to the player cube
        // Mulitplying by Time.deltaTime makes it so
        // that all systems would add force in the same way.
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if(Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
       
    }

}
