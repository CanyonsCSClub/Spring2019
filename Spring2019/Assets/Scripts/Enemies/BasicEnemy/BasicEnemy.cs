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

    public float speed = 5;    //movement speed

    private bool isTracking;
    private bool isWandering;

    void Start()
    {
        enemyBod = GetComponent<Rigidbody>();   //sets physical enemy form to enemyBod variable
        enemyPos = enemyBod.position;
        nextPos = enemyBod.position;
        wanderRad = new Vector3(wanderObj.transform.position.x, 1.5f, wanderObj.transform.position.z);
        // I made a wander radius object so the enemy will only wander in roughly the same area, and even return to that area when it loses track of the player like some game do it. 
        // Otherwise we COULD set the wanderObj to be the same as itself and it'll function the same as before. But idk, I like the idea that it only wanders in roughly the same area. 
        isWandering = true;
        StartCoroutine(GenerateNewWanderPosition()); 
    }

    void Update()
    {
        if (isTracking)
        {
            MoveTowardsPlayer();
        }
        else if (isWandering)
        {
            Wander();
        }
    }

    private void Wander()
    {
        enemyPos = enemyBod.position;
        transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);
    }

    private void MoveTowardsPlayer()
    {
        enemyPos = enemyBod.position;
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
        nextPos = new Vector3(playerPos.x, enemyPos.y, playerPos.z);
        transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);
    }

    public void OnTriggerEnter(Collider otherChar)
    {
        if (otherChar.tag == "Player")
        {
            isWandering = false; 
            isTracking = true;
        }

    }

    public void OnTriggerExit(Collider otherChar)
    {
        if (otherChar.tag == "Player")
        {
            isWandering = true; 
            isTracking = false;
        }
    }

    IEnumerator GenerateNewWanderPosition()
    {
        float moveX = 0.0f;
        float moveZ = 0.0f;
        int rand = 0; 

        while (0 < 1)
        {
            enemyPos = enemyBod.position;

            rand = Random.Range(0, 2); 
            if (rand == 0)
            { moveX = wanderRad.x - Random.Range(3, 7); }   // I went with from 3 to 6 away because of the chance that it might be the same coords as it already is (or only like slightly smaller) 
            else if (rand == 1)
            { moveX = wanderRad.x + Random.Range(3, 7); }

            rand = Random.Range(0, 2);
            if (rand == 0)
            { moveZ = wanderRad.z - Random.Range(3, 7); }
            else if (rand == 1)
            { moveZ = wanderRad.z + Random.Range(3, 7); }

            nextPos = new Vector3(moveX, enemyPos.y, moveZ);
            yield return new WaitForSeconds(10);
        }
    }
}
