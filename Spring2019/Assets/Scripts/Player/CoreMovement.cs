/*
 * Programmer:   Hunter Goodin 
 * Date Created: 03/12/2019 @  6:00 PM 
 * Last Updated: 03/12/2019 @  9:05 PM by Hunter Goodin
 * File Name:    CoreMovement.cs 
 * Description:  This script will be responsible for the player's movement. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMovement : MonoBehaviour
{
    public float speed = 10;		// How fast the player moves 
    public float airSpeed = 5; 		// How fast the player moves in the air 
    public int jumpForce = 10; 		// How high the player can jump 
    private Rigidbody playerRB;	    // Populated with the rigidbody of the player later... 
    public bool isGrounded; 		// This boolean will be used to check if the player is on the ground 

    
    

    void Start ()
    {
        playerRB = GetComponent<Rigidbody>(); // Set playerRB to the rigidbody of the object this script is attached to 
        
	}

	void Update ()
    {    
        PlayerMovement();		// Every frame, run this method 
	}



    private void PlayerMovement()
    {
        // ground speed 
        if (Input.GetKey("w") && isGrounded) 												// If player is pressing W and isGrounded is true... 
        {
            playerRB.transform.Translate(transform.forward * Time.deltaTime * speed);		// move the player forward "speed" fast 
        }
        if (Input.GetKey("a") && isGrounded) 												// If player is pressing A and isGrounded is true... 
        {
            playerRB.transform.Translate(-transform.right * Time.deltaTime * speed);		// move the player left "speed" fast 
        }
        if (Input.GetKey("s") && isGrounded)  												// If player is pressing S and isGrounded is true... 
        {
            playerRB.transform.Translate(-transform.forward * Time.deltaTime * speed);		// move the player backward "speed" fast 
        }
        if (Input.GetKey("d") && isGrounded) 												// If player is pressing D and isGrounded is true... 
        {
            playerRB.transform.Translate(transform.right * Time.deltaTime * speed);			// move the player right "speed" fast 
        }
        // air speed 
        if (Input.GetKey("w") && !isGrounded) 												// If player is pressing W and isGrounded is not true... 
        {
            playerRB.transform.Translate(transform.forward * Time.deltaTime * airSpeed);	// move the player forward "airSpeed" fast 
        }
        if (Input.GetKey("a") && !isGrounded) 												// If player is pressing A and isGrounded is not true... 
        {
            playerRB.transform.Translate(-transform.right * Time.deltaTime * airSpeed);		// move the player left "airSpeed" fast 
        }
        if (Input.GetKey("s") && !isGrounded) 												// If player is pressing S and isGrounded is not true... 
        {
            playerRB.transform.Translate(-transform.forward * Time.deltaTime * airSpeed);	// move the player backward "airSpeed" fast 
        }
        if (Input.GetKey("d") && !isGrounded) 												// If player is pressing D and isGrounded is not true... 
        {
            playerRB.transform.Translate(transform.right * Time.deltaTime * airSpeed);  	// move the player right "airSpeed" fast 
        }
        // jump
        if (Input.GetKeyDown("space") && isGrounded)										// If player pressed the spacebar and isGrounded is true... 
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 					// send an impulse to the Y axis that is jumpForce strong 
        }
    }

    // Collision detection 

    private void OnCollisionStay(Collision col)	// If this object is colliding with another object... 
    {
        if (col.gameObject.layer == 8)			// and that object is on layer 8 (the terrain layer)... 
        {
            isGrounded = true;					// isGrounded is set to true 
        }
    }

    private void OnCollisionEnter(Collision col) // If this object has just collided with another object... 
    {	
        if(col.gameObject.layer == 8)			 // and that object is on layer 8 (the terrain layer)... 
        {
            isGrounded = true; 					 // isGrounded is set to true 
        }
    }

    private void OnCollisionExit(Collision col)	// If this object is just uncollided with another object... 
    {
        if (col.gameObject.layer == 8)			// and that object is on layer 8 (the terrain layer)... 
        {
            isGrounded = false;					// isGrounded is set to false 
        }
    }
}
