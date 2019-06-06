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
    public Vector3 enemyPos;       // location of enemy self
    public Vector3 playerPos;      // location of player
    public Vector3 nextPos;        // set location for enemy to move to
    public Rigidbody enemyBod;     // physical object variable for enemy self
    public GameObject wanderObj;    // the object you want the enemy to wander around
    private Vector3 wanderRad;      // the radius around the wanderObj
    private GameObject player;

    public float speed = 5;     // movement speed
    public int damage = 5; 

    public bool isTracking;    // player is in sight
    public bool isWandering;   // enemy wanders aimlessly to pass the time.  He's a little bored. 

    public GameObject animObj;
    public float rotSpeed;

    private float distanceToPlayer;
    public float aggroRange;

    public bool wasHit;
    public bool recentHit;
    public bool invincibilityRun;

    private Coroutine lastCo = null;

    public bool isDead; 

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
        StartCoroutine(GenerateNewWanderPosition());    // Starts a coroutine which works in the background to designate positions for enemy to Wander to
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
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
        }
    }

    void RotateToTarget(Vector3 target)                                                         //this function rotates the enemy towards the player based on rotationSpeed
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

    float DistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, enemyBod.transform.position);
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
        float myTime = Time.time;
        wasHit = true;
        StartCoroutine(InvincibilityFrames());
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().DamageAni();
        yield return new WaitForSeconds(0.7f);
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().IdleAni();
        wasHit = false;
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

    public void StartDeath()
    {
        enemyBod.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        StopAllCoroutines();
        StartCoroutine(Dieing());
    }

    IEnumerator Dieing()
    {
        isDead = true;
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().enabled = false;
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().enabled = false;
        animObj.gameObject.GetComponent<MushroomMon_Ani_Test>().DeathAni();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator InvincibilityFrames()                           //GERARDO START
    {
        if (!recentHit)
        {
            recentHit = true;
            gameObject.GetComponent<Health>().invincible = true;
            yield return new WaitForSeconds(1);
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
