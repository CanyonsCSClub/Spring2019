/* 
 * Author: Darrell Wong
 * Start Date: 3/29/2019
 * last updated: 3/29/2019
 * Description:     scripting for the big enemy
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy: MonoBehaviour {

    private Rigidbody enemyRB;                  //rigid body of this enemy. populated in start
    private GameObject player;
    public GameObject spinHitbox;               //attach the hitboxes located in the enemies's prefabs
    public GameObject sweepHitbox;
    private float distance;
    private float angle;
    private bool attacking = false;
    private bool backingAway = false;

    public float rotationSpeed;
    public float forwardSpeed;
    public float backwardSpeed;
    public float aggroRange;
    public float attackRange;
    public float frontCone;
    public float backCone;
    public float backAwayTime;


    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();    //get this (enemy) object
        player = GameObject.Find("Player");     //get the player

        sweepHitbox.GetComponent<Renderer>().material.color = Color.red;
        spinHitbox.GetComponent<Renderer>().material.color = Color.yellow;
    }


    void FixedUpdate()
    {
        distance = distanceFromPlayer();
        angle = playerLocationAngle();

        if (attacking == false)                                        //stops everything while attacking if attacking is true
        {
            if (distance < aggroRange)                                 
            {
                if (distance < attackRange && backingAway == false)      //the attacking boolean = true lets the enemy pause between attacks
                {
                    if (playerLocationAngle() < frontCone)
                    {
                        StartCoroutine(attackSweep());
                    }
                    else
                    {
                        StartCoroutine(attackSpin());
                    }
                    //attackOverhead();
                }


                else if (angle < frontCone)                             //these checks help the ai keep the player infront of the enemy
                {
                    if (backingAway == false)
                    {
                        moveForward();
                    }
                    rotateToPlayer();
                }

                else if (angle > frontCone && angle < 85)
                {
                    rotateToPlayer();
                }

                else if (angle > 85 && playerLocationAngle() < backCone)
                {
                    rotateToPlayer();
                    moveBack();
                }
                else if (angle > backCone)
                {
                    rotateToPlayer();
                }
                

            }

            else
            {
                //idle
            }
        }
    }

    void rotateToPlayer()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);                                               
        newDir.y = 0f;                                                                          //zero out rotation
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    float playerLocationAngle()                                                              //returns the player's angle infront of the enemy. 0 is infront, 180 is the back
    {
        Vector3 playerDir = player.transform.position - enemyRB.transform.position;
        return Mathf.Abs(Vector3.SignedAngle(playerDir, transform.forward, transform.up));
    }

    void moveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, forwardSpeed * Time.deltaTime);
    }

    void moveBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.forward, forwardSpeed * Time.deltaTime);
    }

    float distanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, enemyRB.transform.position);
    }


    IEnumerator attackSweep()                                           //using coroutines to make attack sequences
    {
        GetComponent<Renderer>().material.color = Color.red;
        attacking = true;
        //print("wind up");
        yield return new WaitForSeconds(1f);
        //print("attack sweep");
        sweepHitbox.GetComponent<Renderer>().enabled = true;

        yield return new WaitForSeconds(.5f);
        sweepHitbox.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = Color.white;
        //print("end attack");
        //attacking = false;

        StartCoroutine(backAway());
    }

    IEnumerator attackSpin()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        attacking = true;
        //print("wind up");
        yield return new WaitForSeconds(1f);
        spinHitbox.GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        //print("attack spin");
        for (int i = 0; i < 18; i++)
        {
            enemyRB.transform.Rotate(0, 20, 0, Space.Self);
            yield return new WaitForEndOfFrame();
        }
        spinHitbox.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = Color.white;
        //print("end attack");
        //attacking = false;

        StartCoroutine(backAway());
    }

    void attackOverhead1()
    {
        //print("attack overhead");
    }


    IEnumerator backAway()
    {
        backingAway = true;
        attacking = false;
        for (int i = 0; i < backAwayTime; i++)
        {
            //print("back away");
            yield return new WaitForSeconds(.01f);
        }
        backingAway = false;
    }
}
