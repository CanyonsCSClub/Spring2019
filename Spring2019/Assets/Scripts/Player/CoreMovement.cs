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
    private Rigidbody playerRB;		// Populated with the rigidbody of the player later... 
    private Transform playerT;
    // public bool isGrounded; 		// This boolean will be used to check if the player is on the ground 
    public Animator anim;
    public Transform art;
    public Transform lookN;
    public bool lookNBool;
    public Transform lookNW;
    public bool lookNWBool;
    public Transform lookW;
    public bool lookWBool;
    public Transform lookSW;
    public bool lookSWBool;
    public Transform lookS;
    public bool lookSBool;
    public Transform lookSE;
    public bool lookSEBool;
    public Transform lookE;
    public bool lookEBool;
    public Transform lookNE;
    public bool lookNEBool;
    public AudioSource grassSteps;
    public AudioSource swing;
    public AudioSource hurt; 
    public bool playSteps;
    public bool hasShield;
    public bool canMove;
    public bool isDead;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); // Set playerRB to the rigidbody of the object this script is attached to 
        playerT = GetComponent<Transform>();
        art.LookAt(lookS);
        // grassSteps = GetComponent<AudioSource>();
        canMove = true;
    }

    void FixedUpdate()
    {
        if (!isDead && canMove)
        {
            if (!hasShield)
            {
                PlayerMovement();       // Every frame, run this method 
            }
            else
            {
                PlayerMovementWithShield();
            }
            AnimMeth();
            RotChar();
            AudioSteps();
            GameObject.Find("Player").GetComponent<Shield>().BoolGetter(lookNBool, lookNWBool, lookWBool, lookSWBool, lookSBool, lookSEBool, lookEBool, lookNEBool);

            if (grassSteps.isPlaying == false && playSteps == true)
            {
                grassSteps.Play();
            }
            else if (playSteps == false)
            {
                grassSteps.Stop();
            }
            else if (Input.GetKey("l") && gameObject.GetComponent<UnlockSystem>().GetShieldStatus())
            {
                grassSteps.Stop();
            }
        }
        else if (isDead)
        {
            anim.Play("Die");
        }
    }

    public void HasShieldTog()
    {
        hasShield = true;
    }

    private void RotChar()  // controls where the character is facing
    {
        if (Input.GetKey("l") && Input.GetKey("d"))
        {
            // leave blank
        }
        else if (Input.GetKey("l") && Input.GetKey("a"))
        {
            // leave blank
        }
        else if (Input.GetKey("l") && Input.GetKey("s"))
        {
            // leave blank
        }
        else if (Input.GetKey("l") && Input.GetKey("w"))
        {
            // leave blank
        }
        else if (Input.GetKey("w") && Input.GetKey("d"))
        {
            art.LookAt(lookNE);
            lookNBool = false;
            lookNWBool = false;
            lookWBool = false;
            lookSWBool = false;
            lookSBool = false;
            lookSEBool = false;
            lookEBool = false;
            lookNEBool = true;
        }
        else if (Input.GetKey("s") && Input.GetKey("d"))
        {
            art.LookAt(lookSE);
            lookNBool = false;
            lookNWBool = false;
            lookWBool = false;
            lookSWBool = false;
            lookSBool = false;
            lookSEBool = true;
            lookEBool = false;
            lookNEBool = false;
        }
        else if (Input.GetKey("s") && Input.GetKey("a"))
        {
            art.LookAt(lookSW);
            lookNBool = false;
            lookNWBool = false;
            lookWBool = false;
            lookSWBool = true;
            lookSBool = false;
            lookSEBool = false;
            lookEBool = false;
            lookNEBool = false;
        }
        else if (Input.GetKey("w") && Input.GetKey("a"))
        {
            art.LookAt(lookNW);
            lookNBool = false;
            lookNWBool = true;
            lookWBool = false;
            lookSWBool = false;
            lookSBool = false;
            lookSEBool = false;
            lookEBool = false;
            lookNEBool = false;
        }
        else if (Input.GetKey("w"))
        {
            art.LookAt(lookN);
            lookNBool = true;
            lookNWBool = false;
            lookWBool = false;
            lookSWBool = false;
            lookSBool = false;
            lookSEBool = false;
            lookEBool = false;
            lookNEBool = false;
        }
        else if (Input.GetKey("a"))
        {
            art.LookAt(lookW);
            lookNBool = false;
            lookNWBool = false;
            lookWBool = true;
            lookSWBool = false;
            lookSBool = false;
            lookSEBool = false;
            lookEBool = false;
            lookNEBool = false;
        }
        else if (Input.GetKey("s"))
        {
            art.LookAt(lookS);
            lookNBool = false;
            lookNWBool = false;
            lookWBool = false;
            lookSWBool = false;
            lookSBool = true;
            lookSEBool = false;
            lookEBool = false;
            lookNEBool = false;
        }
        else if (Input.GetKey("d"))
        {
            art.LookAt(lookE);
            lookNBool = false;
            lookNWBool = false;
            lookWBool = false;
            lookSWBool = false;
            lookSBool = false;
            lookSEBool = false;
            lookEBool = true;
            lookNEBool = false;
        }
    }

    private void AnimMeth() // Make Animations pretty pretty
    {
        if (Input.GetKey("l") && gameObject.GetComponent<UnlockSystem>().GetShieldStatus())
        {
            anim.Play("Defend");
        }
        else if (Input.GetKey("w") && !Input.GetKey("l"))
        {
            anim.Play("RunForwardBattle");
        }
        else if (Input.GetKey("a") && !Input.GetKey("l"))
        {
            anim.Play("RunForwardBattle");
        }
        else if (Input.GetKey("d") && !Input.GetKey("l"))
        {
            anim.Play("RunForwardBattle");
        }
        else if (Input.GetKey("s") && !Input.GetKey("l"))
        {
            anim.Play("RunForwardBattle");
        }
        else if (Input.anyKey == false)
        {
            anim.Play("Idle_Battle");
        }
        else if (Input.anyKey == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Battle"))
        {
            // leave empty 
        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey("d")) 												// If player is pressing D and isGrounded is true... 
        {
            playerRB.transform.Translate(transform.right * Time.deltaTime * speed);			// move the player right "speed" fast 
        }
        if (Input.GetKey("s"))  												// If player is pressing S and isGrounded is true... 
        {
            playerRB.transform.Translate(-transform.forward * Time.deltaTime * speed);		// move the player backward "speed" fast 
        }
        if (Input.GetKey("w")) 												// If player is pressing W and isGrounded is true... 
        {
            playerRB.transform.Translate(transform.forward * Time.deltaTime * speed);		// move the player forward "speed" fast 
        }
        if (Input.GetKey("a")) 												// If player is pressing A and isGrounded is true... 
        {
            playerRB.transform.Translate(-transform.right * Time.deltaTime * speed);		// move the player left "speed" fast 
        }
    }

    private void PlayerMovementWithShield()
    {
        if (Input.GetKey("d") && !Input.GetKey("l")) 												// If player is pressing D and isGrounded is true... 
        {
            playerRB.transform.Translate(transform.right * Time.deltaTime * speed);			// move the player right "speed" fast 
        }
        if (Input.GetKey("s") && !Input.GetKey("l"))  												// If player is pressing S and isGrounded is true... 
        {
            playerRB.transform.Translate(-transform.forward * Time.deltaTime * speed);		// move the player backward "speed" fast 
        }
        if (Input.GetKey("w") && !Input.GetKey("l")) 												// If player is pressing W and isGrounded is true... 
        {
            playerRB.transform.Translate(transform.forward * Time.deltaTime * speed);		// move the player forward "speed" fast 
        }
        if (Input.GetKey("a") && !Input.GetKey("l")) 												// If player is pressing A and isGrounded is true... 
        {
            playerRB.transform.Translate(-transform.right * Time.deltaTime * speed);		// move the player left "speed" fast 
        }
    }

    private void AudioSteps()
    {
        if (Input.GetKey("w") == true)
        {
            playSteps = true;
        }
        if (Input.GetKey("a") == true)
        {
            playSteps = true;
        }
        if (Input.GetKey("d") == true)
        {
            playSteps = true;
        }
        if (Input.GetKey("s") == true)
        {
            playSteps = true;
        }
        if (Input.anyKey == false)
        {
            playSteps = false;
        }

    }

    public void MakeDead()
    {
        isDead = true;
    }

    public void PushPlayer(Vector3 direction)
    {
        //if (direction.x < playerT.position.x)
        //{
        //    playerRB.AddForce(-transform.right * 500);
        //}

        // StartCoroutine(Please()); 
        // playerRB.transform.Translate(direction * Time.deltaTime * 50);
        StartCoroutine(Please(direction)); 
    }

    IEnumerator Please(Vector3 direction)
    {
        canMove = false;
        anim.Play("GetHit");
        hurt.Play(); 
        playerRB.AddForce(direction * 500);
        yield return new WaitForSeconds(0.3f);
        canMove = true; 
    }

    // Collision detection 

    //private void OnCollisionStay(Collision col)	// If this object is colliding with another object... 
    //{
    //    if (col.gameObject.layer == 8)			// and that object is on layer 8 (the terrain layer)... 
    //    {
    //        isGrounded = true;					// isGrounded is set to true 
    //    }
    //}

    //private void OnCollisionEnter(Collision col) // If this object has just collided with another object... 
    //{	
    //    if(col.gameObject.layer == 8)			 // and that object is on layer 8 (the terrain layer)... 
    //    {
    //        isGrounded = true;                   // isGrounded is set to true 
    //        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
    //        {
    //            AnimMeth();
    //        }
    //    }
    //}

    //private void OnCollisionExit(Collision col)	// If this object is just uncollided with another object... 
    //{
    //    if (col.gameObject.layer == 8)			// and that object is on layer 8 (the terrain layer)... 
    //    {
    //        isGrounded = false;					// isGrounded is set to false 
    //    }
    //}
}
