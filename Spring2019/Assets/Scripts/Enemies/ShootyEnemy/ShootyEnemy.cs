/* 
 * Author: Darrell Wong using Gerardo's wander scripts
 * Start Date: 5/10/2019
 * last updated: 5/17/2019
 * Description:     scripting for the shooty enemy
 *                 this script is similar to "BigEnemy.cs"
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootyEnemy : MonoBehaviour
{

    private Rigidbody enemyRB;                  //rigid body of this enemy. populated in start
    private GameObject player;
    public GameObject projectile;

    private Vector3 enemyPos;       // location of enemy self
    private Vector3 nextPos;        // set location for enemy to move to
    private Vector3 runPos;
    private Vector3 playerPos;

    // Wander stuff - hunter
    public GameObject wanderObj;    // the object you want the enemy to wander around
    private Vector3 wanderRad;      // the radius around the wanderObj
    public bool isWandering;
    public int aggroRange; 

    public float rotationSpeed;
    public float forwardSpeed;
    public float shootRange;
    public float triggerEscape;
    public float escapeDistance;
    public int escapeSeconds;

    private bool isRunningAway;
    private bool isShooting;
    private Vector3 previousPos;

    [Tooltip("Difficulty value of 0 or 1.")]
    public int difficulty;

    public GameObject anim;

    private Coroutine lastCo = null;
    private Coroutine atkCo = null; 
    public bool wasHit;

    public bool isDead; 

    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();    //get this (enemy) object
        player = GameObject.Find("Player");     //get the player

        wanderRad = new Vector3(wanderObj.transform.position.x, 1.5f, wanderObj.transform.position.z);
        // I made a wander radius object so the enemy will only wander in roughly the same area, and even return to that area when it loses track of the player like some game do it. 
        // Otherwise we COULD set the wanderObj to be the same as itself and it'll function the same as before. But idk, I like the idea that it only wanders in roughly the same area. 
        isWandering = true;
        StartCoroutine(GenerateNewWanderPosition());
        nextPos = wanderRad; 
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            playerPos = player.transform.position;
            enemyPos = transform.position;

            if (!isRunningAway && !isShooting)
            {

                if (DistanceFromPlayer() < triggerEscape)
                {
                    StartCoroutine(RunAway());
                }
                else if (DistanceFromPlayer() < shootRange && !wasHit)
                {
                    //RotateToTarget(playerPos);
                    atkCo = StartCoroutine(Shoot());
                }
                else if (DistanceFromPlayer() > aggroRange)
                {
                    Wander(); 
                }
                else
                {
                    //idle
                    anim.GetComponent<Animation>().CrossFade("Anim_Idle");
                }
            }

            if (isShooting)
            {
                switch (difficulty)
                {
                    case 0:
                        RotateToTarget(playerPos);
                        break;
                    case 1:
                        RotateLeadTarget(playerPos, enemyPos);
                        break;
                    default:
                        RotateToTarget(playerPos);
                        break;
                }
            }

            if (isRunningAway)
            {
                RotateToTarget(runPos);
                MoveForward();
            }
            previousPos = playerPos;
        }
    }

    private void Wander()                                                                       // class for wandering
    {
        enemyPos = enemyRB.position;                                                           // updates enemyPos variable with current enemy position
        transform.position = Vector3.MoveTowards(enemyPos, new Vector3(nextPos.x, enemyPos.y, nextPos.z), Time.deltaTime * forwardSpeed);    // moves enemy to position designated by nextPos variable
        RotateToTarget(nextPos);
        //if (enemyPos.x != nextPos.x && enemyPos.z != nextPos.z)
        //{
        //    RotateToTarget(nextPos);
        //    MoveForward(); 
        //}
    }

    void RotateToTarget(Vector3 target)                                                         //this function rotates the enemy towards the player based on rotationSpeed
    {
        //animator.SetBool("rotating", true);
        Vector3 targetDir = target - enemyRB.transform.position;

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        newDir.y = 0f;                                                                          //zero out rotation here
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    float TargetLocationAngle(Vector3 target)                                                   //returns the player's angle infront of the enemy. 0 is infront, 180 is the back
    {
        Vector3 targetDir = target - enemyRB.transform.position;
        return Mathf.Abs(Vector3.SignedAngle(targetDir, transform.forward, transform.up));
    }

    void RotateLeadTarget(Vector3 player, Vector3 enemy)                                //Here is a bunch of math if you really like math. 
                                                                            //This function calculates the predicted impact point of a projectile on a moving target
    {
        float projectileVelocity = 16;

        var playerVeloc = (player - previousPos) / Time.fixedDeltaTime;
        //print(playerPos);

        Vector3 targetDir = Vector3.Normalize(player - enemy);
        Vector3 targetVelocOrth = Vector3.Dot(playerVeloc, targetDir) * targetDir;

        Vector3 targetVelocTangental = playerVeloc - targetVelocOrth;

        Vector3 shotVelocTangental = targetVelocTangental;

        float shotVelocSpeed = shotVelocTangental.magnitude;

        float shotSpeedOrth = Mathf.Sqrt(projectileVelocity * projectileVelocity + shotVelocSpeed * shotVelocSpeed);
        Vector3 shotVelocOrth = targetDir * shotSpeedOrth;

        //print(shotVelocOrth + shotVelocTangental);
        

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = shotVelocOrth + shotVelocTangental;
        newDir.y = 0f;                                                                          //zero out rotation here
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, forwardSpeed * Time.deltaTime);
        //animator.SetBool("moving forward", true);
    }

    void MoveToLocation(Vector3 target)
    {
        Vector3 dir = (target - this.transform.position).normalized; //unit length vector towards target
        this.transform.position = dir * Time.deltaTime;

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
        if (isShooting)
        {
            StopCoroutine(atkCo);
            isShooting = false;
        }
        anim.GetComponent<Animation>().Play("Anim_Death");
        yield return new WaitForSeconds(.7f);
        anim.GetComponent<Animation>().CrossFade("Anim_Idle");
        wasHit = false;
        // lastCo = null;
    }

    IEnumerator Shoot()                                             //simply instantiate a projectile, the projectile does the moving by itself
    {
        isShooting = true;
        anim.GetComponent<Animation>().CrossFade("Anim_Damage");
        yield return new WaitForSeconds(.3f);
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        isShooting = false;
    }


    IEnumerator RunAway()                                           //using Gerardos wander function as a base, picks a random point and sets it as the next location to move to
    {
        isRunningAway = true;
        anim.GetComponent<Animation>().CrossFade("Anim_Run");

        float moveX = 0.0f;
        float moveZ = 0.0f;
        int rand = 0;


            //enemyPos = enemyRB.position;                       // updates enemyPos variable with current enemy location

            rand = Random.Range(0, 2);                          // randomly chooses whether enemy moves in positive or negative x direction.
            if (rand == 0)                                      // 0 = negative
            { moveX = playerPos.x - Random.Range(escapeDistance - 3, escapeDistance);
            }       // I went with from 3 to 6 away because of the chance that it might be the same coords as it already is (or only like slightly smaller) 
            else if (rand == 1)                                 // 1 = positive
            { moveX = playerPos.x + Random.Range(escapeDistance - 3, escapeDistance);
            }       // chooses random distance for movement 

            rand = Random.Range(0, 2);                          // randomly chooses whether enemy moves in positive or negative y direction.
            if (rand == 0)                                      // 0 = negative
            { moveZ = playerPos.z - Random.Range(escapeDistance - 3, escapeDistance);
            }
            else if (rand == 1)                                 // 1 = positive
            { moveZ = playerPos.z + Random.Range(escapeDistance-3, escapeDistance);
            }

            runPos = new Vector3(moveX, enemyPos.y, moveZ);    // updates nextPos variable with randomly chosen coordinates


            yield return new WaitForSeconds(escapeSeconds);
        isRunningAway = false;
    }

    public void StartDeath()
    {
        enemyRB.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        StopAllCoroutines();
        StartCoroutine(Dieing());
    }

    // Hunter was here
    IEnumerator Dieing()
    {
        isDead = true;
        anim.GetComponent<Animation>().enabled = false;
        anim.GetComponent<Animation>().enabled = true;
        anim.GetComponent<Animation>().Play("Anim_Death");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
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
}