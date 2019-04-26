/*
 * Programmer:   Gerardo Bonnet based on code by Spencer Wilson
 * Date Created: 03/15/2019 @  11:30 PM 
 * Last Updated: 03/21/2019 @  08:26 PM by Hunter Goodin
 * File Name:    CoreMovement.cs 
 * Description:  Movement of enemies at the basic level. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{ 
    private Vector3 enemyPos;       // location of enemy self
    private Vector3 playerPos;      // location of player
    private Vector3 nextPos;        // set location for enemy to move to
    private Rigidbody enemyBod;     // physical object variable for enemy self
    public GameObject wanderObj;    // the object you want the enemy to wander around
    private Vector3 wanderRad;      // the radius around the wanderObj

    public float speed = 5;     // movement speed

    private bool isTracking;    // player is in sight
    private bool isWandering;   // enemy wanders aimlessly to pass the time.  He's a little bored.

    void Start()
    {
        enemyBod = GetComponent<Rigidbody>();           // sets physical enemy form to enemyBod variable
        enemyPos = enemyBod.position;                   // sets enemy position coordinates to enemyPos variable
        nextPos = enemyBod.position;                    // sets enemy position to nextPos variable
        wanderRad = new Vector3(wanderObj.transform.position.x, 1.5f, wanderObj.transform.position.z);
        // I made a wander radius object so the enemy will only wander in roughly the same area, and even return to that area when it loses track of the player like some game do it. 
        // Otherwise we COULD set the wanderObj to be the same as itself and it'll function the same as before. But idk, I like the idea that it only wanders in roughly the same area. 
        isWandering = true;
        StartCoroutine(GenerateNewWanderPosition());    // Starts a coroutine which works in the background to designate positions for enemy to Wander to
    }

    void Update()
    {
        if (isTracking)             // enemy is looking for the player. enemy is lonely
        {
            MoveTowardsPlayer();    // if enemy sees the player, enemy moves toward the player.  enemy likes hugs
        }
        else if (isWandering)       // enemy starts wandering around if the player is not nearby.  standing around doing nothing all day sucks.
        {
            Wander();
        }
    }

    private void Wander()                                                                       // class for wandering
    {
        enemyPos = enemyBod.position;                                                           // updates enemyPos variable with current enemy position
        transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);    // moves enemy to position designated by nextPos variable
                                                                                                // first variable enemyPos is enemy current position, second variable nextPos is location enemy will be moving to, Time.deltaTime * speed moves the enemy at the same speed regardless of framerate
    }

    private void MoveTowardsPlayer()                                                            // movement
    {
        enemyPos = enemyBod.position;                                                           // updates enemyPos variable with current enemy position
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;               // finds location of player object and sets it to playerPos variable
        nextPos = new Vector3(playerPos.x, enemyPos.y, playerPos.z);                            // sets x, y, and z coordinates from playerPos variable to nextPos variable
        transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);    // moves enemy to position designated by nextPos variable
    }

    public void OnTriggerEnter(Collider otherChar)  // detects player entering radius around enemy
    {
        if (otherChar.tag == "Player")              // checks tag of entering object to see if it's the player
        {
            isWandering = false;                    // sets enemy to stop wandering
            isTracking = true;                      // sets enemy to follow player
        }

    }

    public void OnTriggerExit(Collider otherChar)               // detects player leaving radius around enemy
    {
        if (otherChar.tag == "Player")                          // checks tag of exiting object to see if it's the player
        {
            isWandering = true;                                 // sets enemy to wander
            isTracking = false;                                 // sets enemy to stop following player
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().AddjustCurrentHealth(-5);
            print("taking damage");
        }
    }

    IEnumerator GenerateNewWanderPosition()                     // coroutine to set locations for enemy to wander to
    {
        float moveX = 0.0f;
        float moveZ = 0.0f;
        int rand = 0; 

        while (0 < 1)
        {
            enemyPos = enemyBod.position;                       // updates enemyPos variable with current enemy location

            rand = Random.Range(0, 2);                          // randomly chooses whether enemy moves in positive or negative x direction.
            if (rand == 0)                                      // 0 = negative
            { moveX = wanderRad.x - Random.Range(3, 7); }       // I went with from 3 to 6 away because of the chance that it might be the same coords as it already is (or only like slightly smaller) 
            else if (rand == 1)                                 // 1 = positive
            { moveX = wanderRad.x + Random.Range(3, 7); }       // chooses random distance for movement 

            rand = Random.Range(0, 2);                          // randomly chooses whether enemy moves in positive or negative y direction.
            if (rand == 0)                                      // 0 = negative
            { moveZ = wanderRad.z - Random.Range(3, 7); }
            else if (rand == 1)                                 // 1 = positive
            { moveZ = wanderRad.z + Random.Range(3, 7); }

            nextPos = new Vector3(moveX, enemyPos.y, moveZ);    // updates nextPos variable with randomly chosen coordinates
            yield return new WaitForSeconds(10);                // enemy waits 10 seconds before choosing new position
        }
    }
}
