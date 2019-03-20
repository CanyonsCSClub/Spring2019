/*
 * Programmer:   Gerardo Bonnet based on code by Spencer Wilson
 * Date Created: 03/15/2019 @  11:30 PM 
 * Last Updated: 03/15/2019 @   PM by Gerardo Bonnet
 * File Name:    CoreMovement.cs 
 * Description:  Movement of enemies at the basic level. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMovement_Enemy : MonoBehaviour/* CoreMovement */{

    private Vector3 enemyPos;   //location of enemy self
    private Vector3 playerPos;  //location of player
    private Vector3 nextPos;    //set location for enemy to move to
    private Rigidbody enemyBod; //physical object variable for enemy self
    private float speed = 5;    //movement speed
    public int health = 3;
    
    private bool isTracking = false;

    private bool DEBUG = false;

    public bool isWorking;



    void Start()
    {
        enemyBod = GetComponent<Rigidbody>();   //sets physical enemy form to enemyBod variable
        StartCoroutine(Wander());
    }

    public void DecrementHealth(int passed)
    {
        health = health - passed; 
    }

    void Update()
    {
        
        if (isTracking)
        {
            Movement_Enemy();
        }
        
        
        
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    public void Movement_Enemy()
    {
        
            enemyPos = enemyBod.position;
            playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
            nextPos = new Vector3(playerPos.x, 0f, playerPos.z);
            transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);
        
    }

    public void OnTriggerEnter(Collider otherChar)
    {
        if (otherChar.tag == "Player")
        {
            isTracking = true;
        }

    }

    public void OnTriggerExit(Collider otherChar)
    {
        if (otherChar.tag == "Player")
        {
            isTracking = false;
        }
    }

    public IEnumerator Wander()
    {
        while (0 < 1)
        {
            if (!isTracking)
            {

                print("wander");
                switch (Random.Range(0, 2))
                {
                    case 0:
                        yield return new WaitForSecondsRealtime(1);
                        print("wait");
                        break;

                    case 1:
                        enemyPos = enemyBod.position;
                        nextPos = new Vector3(enemyPos.x + Random.Range(-50, 50), 0f, enemyPos.z + Random.Range(-50, 50));
                        transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);
                        print("go");
                        isWorking = !isWorking; 
                        yield return new WaitForSecondsRealtime(1);
                        break;
                }
            }
        }
    }
        
}
