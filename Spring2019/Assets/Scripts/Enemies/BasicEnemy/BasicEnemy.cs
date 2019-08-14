/*
 * Programmer:   Gerardo Bonnet, partly based on code by Spencer Wilson, Hunter Goodin, and Darrell Wong
 * Date Created: 03/15/2019 @  11:30 PM 
 * Last Updated: 08/13/2019 @  02:35 PM by Hunter Goodin
 * File Name:    BasicEnemy.cs 
 * Description:  The behavior of the basic enemy. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Vector3 enemyPos;       // location of this enemy 
    public Vector3 playerPos;      // location of the player
    public Vector3 nextPos;        // set location for this enemy to move to
    public Rigidbody enemyBod;     // physical object variable for enemy self
    public GameObject wanderObj;   // the object you want the enemy to wander around
    private Vector3 wanderRad;     // the radius around the wanderObj (technically a square)
    private GameObject player;

    public float speed = 5;     // movement speed
    public int damage = 5;      // Damage dealt (Hunter was here)

    public bool isTracking;    // player is in sight
    public bool isWandering;   // enemy wanders aimlessly to pass the time.  He's a little bored. 

    // Hunter was here start... 
    public GameObject animObj;
    public float rotSpeed;

    private float distanceToPlayer;
    public float aggroRange;

    public bool wasHit;
    public bool recentHit;

    private Coroutine lastCo = null;

    public bool isDead;

    public AudioSource hit; 

    // Hunter was here end... 

    void Start()
    {
        enemyBod = GetComponent<Rigidbody>();           // sets physical enemy form to enemyBod variable
        player = GameObject.Find("Player");     //get the player
        enemyPos = enemyBod.position;                   // sets enemy position coordinates to enemyPos variable
        nextPos = enemyBod.position;                    // sets enemy position to nextPos variable
        wanderRad = new Vector3(wanderObj.transform.position.x, 1.5f, wanderObj.transform.position.z);
        // I made a wander radius object so the enemy will only wander in roughly the same area, and even return to that area when it loses track of the player like some game do it. 
        // Otherwise we COULD set the wanderObj to be the same as itself and it'll function the same as before. But idk, I like the idea that it only wanders in roughly the same area. 
        isWandering = true;
        recentHit = false;
        nextPos = gameObject.GetComponent<Transform>().position;
        StartCoroutine(GenerateNewWanderPosition());    // Starts a coroutine which works in the background to designate positions for enemy to Wander to
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            // Hunter was here start... 
            distanceToPlayer = DistanceFromPlayer();

            if (distanceToPlayer < aggroRange)
            { isTracking = true; }
            else
            { isTracking = false; }

            if (isTracking)             // enemy is looking for the player. enemy is lonely
            {
                MoveTowardsPlayer();    // if enemy sees the player, enemy moves toward the player.  enemy likes hugs
                animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().RunAni();
                RotateToTarget(playerPos);
            }
            else if (enemyPos.x == nextPos.x && enemyPos.z == nextPos.z)       // enemy starts wandering around if the player is not nearby.  standing around doing nothing all day sucks.
            {
                Wander();
                animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().IdleAni();
            }
            else if (enemyPos.x != nextPos.x && enemyPos.z != nextPos.z)
            {
                Wander();
                animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().RunAni();
                RotateToTarget(nextPos);
            }
            // Hunter was here end... 
        }
    }

    //this function rotates the enemy towards the player based on rotationSpeed (Hunter was here but based it partially on Darrell's code) 
    void RotateToTarget(Vector3 target) 
    {
        Vector3 targetDir = target - transform.position;

        float step = rotSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        newDir.y = 0f;                                                                          //zero out rotation here
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void Wander()                                                                       // class for wandering
    {
        enemyPos = enemyBod.position;                                                           // updates enemyPos variable with current enemy position
        transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);    // moves enemy to position designated by nextPos variable
    }

    private void MoveTowardsPlayer()                                                            // movement
    {
        enemyPos = enemyBod.position;                                                           // updates enemyPos variable with current enemy position
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;               // finds location of player object and sets it to playerPos variable
        nextPos = new Vector3(playerPos.x, enemyPos.y, playerPos.z);                            // sets x, y, and z coordinates from playerPos variable to nextPos variable
        if (!wasHit)
        {
            transform.position = Vector3.MoveTowards(enemyPos, nextPos, Time.deltaTime * speed);    // moves enemy to position designated by nextPos variable
        }
    }

    float DistanceFromPlayer()      // Calculate the distance this object is from the player (Hunter was here)
    {
        return Vector3.Distance(player.transform.position, enemyBod.transform.position);    // Return the distance 
    }

    public void StartHit()  // Start the damaged animation (Hunter was here) 
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

    IEnumerator GetHit()    // (Hunter was here) 
    {
        wasHit = true;                                                          // Set wasHit to true so the hit will start (this is useful for other aspects of the process)
        hit.Play();                                                             // Start the hit sound effect
        StartCoroutine(InvincibilityFrames());                                  // Make it so it can't recieve more damage for some time 
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().DamageAni();    // Play the damaged animation
        yield return new WaitForSeconds(0.7f);                                  // let the animation play out...
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().IdleAni();      // return to idle once animation has been played
        wasHit = false;                                                         // Set wasHit back to false so the hit is over 
    }

    IEnumerator GenerateNewWanderPosition()                     // coroutine to set locations for enemy to wander to (Hunter was here) 
    {
        float moveX = 0.0f;
        float moveZ = 0.0f;
        int rand = 0;
        int randSec = 0;

        while (0 < 1)
        {
            randSec = Random.Range(5, 10);
            yield return new WaitForSeconds(randSec);                // enemy waits 10 seconds before choosing new position

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
        }
    }

    public void StartDeath()    // Behavior when the enemy dies (Hunter was here) 
    {
        enemyBod.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;   // Freeze the body so it can't move 
        StopAllCoroutines();        // Stop every coroutine from this script
        StartCoroutine(Dieing());   // Start the dieing coroutine 
    }   

    IEnumerator Dieing()
    {
        isDead = true;                                                              // set isDead to true 
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().enabled = false;    // Stop  all animations 
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().DeathAni();         // Start death animation 
        yield return new WaitForSeconds(2);                                         // Wait 2 second for animation to play out 
        Destroy(gameObject);                                                        // Destroy the enemy object 
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

    #region Old
    //private void OnCollisionEnter(Collision col)
    //{

    //    if (!colList.Contains(col))
    //    {
    //        colList.Add(col);
    //    }

    //    if (col.gameObject.name == "Player" && CheckList("Shield(Clone)") == false)
    //    {
    //        col.gameObject.GetComponent<Health>().ChangeHealth(-damage);
    //    }

    //    //if (col.gameObject.CompareTag("Player"))
    //    //{
    //    //    col.gameObject.GetComponent<Health>().ChangeHealth(-5);
    //    //    print("taking damage");
    //    //}
    //}

    //private void OnCollisionExit(Collision col)
    //{
    //    if (colList.Contains(col))
    //    {
    //        colList.Remove(col);
    //    }
    //}

    //private bool CheckList(string objName)
    //{
    //    bool check = false;
    //    for (int i = 0; i < colList.Count; i++)
    //    {
    //        if (colList[i].gameObject.name == objName)
    //        {
    //            check = true;
    //            i = colList.Count;
    //        }
    //        else
    //        {
    //            check = false;
    //        }
    //    }
    //    return check;
    //}
    #endregion
}
