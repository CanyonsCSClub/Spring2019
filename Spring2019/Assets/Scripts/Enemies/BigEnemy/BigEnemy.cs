/* 
 * Author: Darrell Wong
 * Start Date: 3/29/2019
 * last updated: 4/12/2019
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

    public Animator animator;
    
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

        animator.SetBool("moving forward", false);                      //I wasn't sure how to rig the animations properly. it works but dont follow my example because i might be wrong.
        animator.SetBool("backaway", backingAway);


        if (attacking == false)                                        //stops everything while attacking if attacking is true
        {
            if (distance < aggroRange)                                 
            {

                animator.SetBool("in range", true);

                if (distance < attackRange && backingAway == false)      //if the player is in the attack range (very close), the enemy will either "sweepattack" or "spin attack" based on if the player is infront or behind
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


                else if (angle < frontCone)                             //if the player is infornt of the enemy, it will walk forward and rotate
                {
                    if (backingAway == false)
                    {
                        moveForward();
                    }
                    rotateToPlayer();                   
                }

                else if (angle > frontCone && angle < 85)               //if the player is to the front side of the enemy it will rotate
                {
                    rotateToPlayer();
                }

                else if (angle > 85 && playerLocationAngle() < backCone)    //if the player is to the side of the enemy, it will walk backwards
                {
                    rotateToPlayer();
                    moveBack();
                }
                else if (angle > backCone)                                  //if the player is behind the enemy, it will rotate
                {
                    rotateToPlayer();
                }
                

            }

            else
            {
                animator.SetBool("in range", false);
                //idle
            }
        }
    }

    void rotateToPlayer()                                                                       //this function rotates the enemy towards the player based on rotationSpeed
    {
        Vector3 targetDir = player.transform.position - transform.position;

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);                                               
        newDir.y = 0f;                                                                          //zero out rotation here
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
        animator.SetBool("moving forward", true);
    }

    void moveBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.forward, forwardSpeed * Time.deltaTime);
    }

    float distanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, enemyRB.transform.position);
    }


    IEnumerator attackSweep()                                           //using coroutines to make attack sequences. Coroutines allow the sequence to have timing delays.
    {
        animator.SetTrigger("sweep attack");

        GetComponent<Renderer>().material.color = Color.red;
        attacking = true;
        //print("wind up");
        yield return new WaitForSeconds(.8f);
        //print("attack sweep");
        sweepHitbox.GetComponent<Renderer>().enabled = true;

        yield return new WaitForSeconds(.2f);
        sweepHitbox.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(.7f);

        GetComponent<Renderer>().material.color = Color.white;
        //print("end attack");
        //attacking = false;

        //animator.SetBool("sweep attack", false);
        StartCoroutine(backAway());
    }

    IEnumerator attackSpin()
    {
        animator.SetTrigger("spin attack");

        GetComponent<Renderer>().material.color = Color.yellow;
        attacking = true;
        //print("wind up");
        yield return new WaitForSeconds(1f);
        spinHitbox.GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        //print("attack spin");

        for (int i = 0; i < 18; i++)
        {
            enemyRB.transform.Rotate(0, -20, 0, Space.Self);
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


    IEnumerator backAway()                              //this coroutine is activated after an attack. It gives a delay between attacks.
    {
        backingAway = true;
        //animator.SetBool("backaway", true);
        attacking = false;

        yield return new WaitForSeconds(backAwayTime);

        backingAway = false;
        //animator.SetBool("backaway", false);

    }
}
