/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  04:35 PM by Hunter Goodin
 * File Name:    BossFire.cs 
 * Description:  This script will be responsible for handling the behavior 
 *                of the fire spawned by the boss' Flare attack. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    public float speed = 4;     // How fast the fire can move 
    public Vector3 playerPos;   // The position of the player to be populated in-script (public for debugging purposes)
    private Rigidbody rb;       // Reference to this rigidbody to be populated in-script 

	void Start ()
    {
        rb = GetComponent<Rigidbody>();                                             // Set rb = to the rigidbody this script is attached to 
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;   // Set playerPos = to the player's current position 
        Destroy(gameObject, 2f);                                                    // Destroy this obj after 2 seconds 
	}

	void Update ()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;                                                   // Set playerPos = to the player's current position 
        transform.position = Vector3.MoveTowards(gameObject.GetComponent<Transform>().position, playerPos, Time.deltaTime * speed); // Move this transform towards the player speed fast 

        if (GameObject.Find("Orcutoryx The Scourge").GetComponent<BossEnemy>().IsBossDead())    // If the boss obj is dead... 
        {
            Destroy(gameObject);                                                                // Destroy this obj 
        }
    }

    private void OnTriggerEnter(Collider col)                                                   // If this obj touches a collision... 
    {
        if (col.gameObject.name == "Shield(Clone)")                                             // if the collision is the shield... 
        {   
            Destroy(gameObject);                                                                // destroy this obj
        }
        if (col.gameObject.tag == "Walls")                                                      // If this obj touches a wall... 
        {
            Destroy(gameObject);                                                                // destroy this obj 
        }
    }
}
