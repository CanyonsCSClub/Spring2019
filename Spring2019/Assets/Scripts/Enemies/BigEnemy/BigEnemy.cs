/* 
 * Author: Darrell Wong using Gerardo's wander scripts
 * Start Date: 3/29/2019
 * last updated: 4/19/2019
 * Description:     scripting for the big enemy
 */

// #pragma strict
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy: MonoBehaviour
{
    private Rigidbody enemyRB;                  //rigid body of this enemy. populated in start
    private GameObject player;
    public GameObject spinHitbox;               //attach the hitboxes located in the enemies's prefabs
    public GameObject sweepHitbox;

    public GameObject wanderObj;    // the object you want the enemy to wander around
    private Vector3 wanderRad;      // the radius around the wanderObj
    private bool isWandering;
    private Vector3 enemyPos;       // location of enemy self
    private Vector3 nextPos;        // set location for enemy to move to
    private Vector3 playerPos;

    private float distanceToPlayer;
    private float distanceToTarget;
    private float angle;
    private bool attacking = false;
    private bool coolDown = false;

    public float rotationSpeed;
    public float forwardSpeed;
    public float backwardSpeed;
    public float aggroRange;
    public float attackRange;
    public float frontCone;
    public float backCone;
    public float CoolDownTime;

    public Animator animator;

    public bool isSweeping;
    public bool isSpinning;

    private Coroutine lastCo = null;
    private Coroutine AtkCo = null; 
    public bool wasHit;
    public bool recentHit;

    public bool isDead; 

    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();    //get this (enemy) object
        player = GameObject.Find("Player");     //get the player

        sweepHitbox.GetComponent<Renderer>().material.color = Color.red;
        spinHitbox.GetComponent<Renderer>().material.color = Color.yellow;

        wanderRad = new Vector3(wanderObj.transform.position.x, 1.5f, wanderObj.transform.position.z);
        // I made a wander radius object so the enemy will only wander in roughly the same area, and even return to that area when it loses track of the player like some game do it. 
        // Otherwise we COULD set the wanderObj to be the same as itself and it'll function the same as before. But idk, I like the idea that it only wanders in roughly the same area. 
        isWandering = true;
        recentHit = false;
        StartCoroutine(GenerateNewWanderPosition());    // Starts a coroutine which works in the background to designate positions for enemy to Wander to
    }


    void FixedUpdate()
    {
        if (!isDead)
        {
            playerPos = player.transform.position;
            distanceToPlayer = DistanceFromPlayer();

            animator.SetBool("moving forward", false);                      //I wasn't sure how to rig the animations properly. it works but dont follow my example because i might be wrong.
            animator.SetBool("rotating", false);
            animator.SetBool("CoolDown", coolDown);

            if (attacking == false)                                        //stops everything while attacking if attacking is true
            {
                if (distanceToPlayer < aggroRange)                         //player is very close to the enemy, enemy is annoyed with you ever since that prank you pulled that last Christmas party 
                {
                    animator.SetBool("in range", true);
                    MoveToTarget(playerPos);                               //Move towards the player
                }

                else
                {
                    animator.SetBool("in range", false);

                    MoveToTarget(nextPos);


                    //idle
                }
            }
        }
    }

    void MoveToTarget(Vector3 target)
    {
        angle = TargetLocationAngle(target);
        distanceToTarget = Vector3.Distance(target, enemyRB.transform.position); ;

        if (distanceToTarget < attackRange && coolDown == false)      //if the player is in the attack range (very close), the enemy will either "sweepattack" or "spin attack" based on if the player is infront or behind
        {
            if (distanceToPlayer < aggroRange)                           //if player is very close to the enemy, the player is close enough to get a wiff of enemy's Vercase cologne
            {
                if (TargetLocationAngle(target) < frontCone)             //checks if the player is infront of the enemy
                {
                    StartCoroutine(AttackSweep());                       //start the front attack sequence, enemy is annoyed with you ever since that prank you pulled on that last Christmas party
                }
                else                                                     //else if the player is behind the enemy
                {
                    StartCoroutine(AttackSpin());                        //start the spin attack if player is behind enemy. enemy wants to one up your dance skills by showing off his sick breakdancing moves
                }
            }
            else if (distanceToTarget < aggroRange)
            {
                //do nothing because idle
            }
        }

        //these are the scripting of the movement while the enemy is on cooldown after attacking

        else if (angle < frontCone)                             //if the player is infornt of the enemy, it will walk forward and rotate
        {
            if (coolDown == false)
            {
                MoveForward();
            }
            RotateToTarget(target);
        }

        else if (angle > frontCone && angle < 85)               //if the player is to the front side of the enemy it will rotate
        {
            RotateToTarget(target);
        }

        else if (angle > 85 && TargetLocationAngle(target) < backCone)    //if the player is to the side of the enemy, it will walk backwards
        {
            RotateToTarget(target);
            MoveBack();
        }
        else if (angle > backCone)                                  //if the player is behind the enemy, it will rotate
        {
            RotateToTarget(target);
        }
    }

    void RotateToTarget(Vector3 target)                                                         //this function rotates the enemy towards the player based on rotationSpeed
    {
        animator.SetBool("rotating", true);
        Vector3 targetDir = target - transform.position;

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);                                               
        newDir.y = 0f;                                                                          //zero out rotation here
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    float TargetLocationAngle(Vector3 target)                                                              //returns the player's angle infront of the enemy. 0 is infront, 180 is the back
    {
        Vector3 targetDir = target - enemyRB.transform.position;
        return Mathf.Abs(Vector3.SignedAngle(targetDir, transform.forward, transform.up));
    }

    void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, forwardSpeed * Time.deltaTime);
        animator.SetBool("moving forward", true);
    }

    void MoveBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.forward, forwardSpeed * Time.deltaTime);
    }

    float DistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, enemyRB.transform.position);
    }

    public void StartHit()
    {
        if (!wasHit)
        {
            // StopCoroutine(lastCo);
            lastCo = StartCoroutine(GetHit());
        }
        else if (wasHit)
        {
            StopCoroutine(lastCo);
            wasHit = true;
            lastCo = StartCoroutine(GetHit());
        }
    }

    IEnumerator GetHit()
    {
        wasHit = true;
        StartCoroutine(InvincibilityFrames());
        if (isSweeping)
        {
            StopCoroutine(AttackSweep());
            isSweeping = false; 
        }
        if (isSpinning)
        {
            StopCoroutine(AttackSpin());
            isSpinning = false; 
        }
        animator.Play("GetHit");
        yield return new WaitForSeconds(.7f);
        animator.Play("Idle");
        wasHit = false;
        // lastCo = null;
    }

    IEnumerator AttackSweep()                                           //using coroutines to make attack sequences. Coroutines allow the sequence to have timing delays.
    {
        isSweeping = true; 
        animator.SetTrigger("sweep attack");

        GetComponent<Renderer>().material.color = Color.red;
        attacking = true;
                                                                        //print("wind up");
        yield return new WaitForSeconds(.8f);
        sweepHitbox.SetActive(true);

        yield return new WaitForSeconds(.2f);                           //active hitbox
        sweepHitbox.gameObject.GetComponent<DamageCollider>().EmptyList();
        sweepHitbox.SetActive(false); 
        yield return new WaitForSeconds(.7f);
        
        GetComponent<Renderer>().material.color = Color.white;
                                                                        //print("end attack");
        StartCoroutine(CoolDown());
        isSweeping = false;
        // lastCo = null; 
    }

    IEnumerator AttackSpin()
    {
        isSpinning = true; 
        animator.SetTrigger("spin attack");

        GetComponent<Renderer>().material.color = Color.yellow;
        attacking = true;
                                                                        //print("wind up");
        yield return new WaitForSeconds(1f);
        //spinHitbox.GetComponent<Renderer>().enabled = true;
        spinHitbox.SetActive(true);
        GetComponent<Renderer>().material.color = Color.yellow;
                                                                        //print("active hitbox");

        for (int i = 0; i < 18; i++)
        {
            enemyRB.transform.Rotate(0, -20, 0, Space.Self);
            yield return new WaitForEndOfFrame();
        }
        spinHitbox.SetActive(false);
        //spinHitbox.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = Color.white;
                                                                        //print("end attack");
        StartCoroutine(CoolDown());
        isSpinning = false;
        // lastCo = null; 
    }

    void attackOverhead1()
    {
        //print("attack overhead");
    }


    IEnumerator CoolDown()                                      //this coroutine is activated after an attack. It gives a delay between attacks. Enemy can move while on cooldown
    {
        coolDown = true;
        //animator.SetBool("CoolDown", true);
        attacking = false;

        yield return new WaitForSeconds(CoolDownTime);

        coolDown = false;
        //animator.SetBool("CoolDown", false);

    }

    IEnumerator GenerateNewWanderPosition()                     // coroutine to set locations for enemy to wander to
    {
        float moveX = 0.0f;
        float moveZ = 0.0f;
        int rand = 0;

        while (0 < 1)
        {
            enemyPos = enemyRB.position;                       // updates enemyPos variable with current enemy location

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

    public void StartDeath()
    {
        enemyRB.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        StopAllCoroutines();
        StartCoroutine(Dieing()); 
    }

    IEnumerator Dieing()
    {
        isDead = true;
        animator.enabled = false;
        animator.enabled = true;
        animator.Play("Die"); 
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator InvincibilityFrames()                           //GERARDO START
    {
        if (!recentHit)
        {
            recentHit = true;
            gameObject.GetComponent<Health>().invincible = true;
            yield return new WaitForSeconds(.6f);
            gameObject.GetComponent<Health>().invincible = false;
            recentHit = false;
        }
    }                                                           //GERARDO END
}
