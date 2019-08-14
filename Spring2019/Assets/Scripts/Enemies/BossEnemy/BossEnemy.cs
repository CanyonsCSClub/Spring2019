/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  03:00 PM by Hunter Goodin
 * File Name:    BossEnemy.cs 
 * Description:  This script controlls the boss' behavior. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    // this declarations
    public Rigidbody thisRB;
    public Vector3 thisPos;
    public Animator anim;

    // player declarations
    public GameObject playerObj;
    public Rigidbody playerRB;
    public Vector3 playerPos;

    // this vars 
    public bool isActive;
    public float walkSpeed;
    public float rotSpeed;
    public float angle;

    // attack stuff 
    public float poundRange;            // The range that the pound attack can be used to be given value in-engine 
    public float fireRange;             // The range that the fire attack can be used to be given value in-engine 
    public bool cooldown;               // The cooldown time in between attacks to be given value in-engine 
    private Coroutine lastCo = null;    // A coroutine referencing the last coroutine used 
    public bool isGrdPnd;               // When this is true, the boss will use a ground pound attack 
    public bool isFlare;                // When this is true, the boss will use the fire attack 
    public bool attacking;              // When this is true, the boss is using an attack 
    public int cooldownTime;            // A separate cooldown time variable to be given value in-engine 
    public GameObject fireHand;         // The hand that the fire will come out of to be populated in-engine 
    public GameObject fire;             // The fire prefab to be populated in-engine 
    public GameObject poundHand;        // The hand the ground pound will come out of to be populated in-engine 
    public GameObject poundWave;        // The wave from the ground pount prefab to be populated in-engine 
    public AudioSource flareSFX;        // The sound of the fire 
    public AudioSource deathSFX;        // The sound played on death 
    public AudioSource song;            // The boss music 
    public bool testingFlare;       // Variable used for testing the Flare attack 
    public bool testingGrdPnd;      // Variable used for testing the Ground Pound attack 

    public bool isDead;

    private void Start()
    {
        thisRB = GetComponent<Rigidbody>();                             // Set thisRB to the rigidbody this script is attached to 
        playerObj = GameObject.Find("Player");                          // Set playerObj to the player object 
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>(); // Set playerRB to the player's rigidbody 
        anim = GetComponent<Animator>();                                // Set anim to the animator this script is attached to 
    }

    void Update ()
    {
        if (isActive)                       // If the boss is active... 
        {
            thisPos = thisRB.position;      // thisPos = the boss's coords
            playerPos = playerRB.position;  // playerPos = the player's coords
            MoveToTarget(playerPos);        // call the MoveToTarget function to move towards the player's coords
            if (!attacking)                 // If this enemy is not attacking... 
            {
                RotateToTarget(playerPos);  // Call the RotateToTarget function to rotate towards the player 
            }
            AttackRangeChecker(playerPos);  // Call the AttackRangeChecker function to check the distance to player 
        }
    }

    void AttackRangeChecker(Vector3 target)
    {
        float distanceToTarget = Vector3.Distance(target, thisRB.transform.position);               // Set the distance to the player

        if (distanceToTarget < poundRange && cooldown == false && attacking == false && !isDead)    // If the distance to player is within poundRange, the boss is not cooling down, and not attacking, and is not dead... 
        {
            lastCo = StartCoroutine(GroundPound());                                                 // start the GroundPound coroutine 
            testingGrdPnd = false;                                                                  // Set testingGrdPnd to false for good measure 
        }
        if (distanceToTarget < fireRange && cooldown == false && attacking == false && !isDead)     // If the distance to player is within fireRange, the boss is not cooling down, and not attacking, and is not dead... 
        {
            lastCo = StartCoroutine(Flare());                                                       // start the Flare coroutine 
            testingFlare = false;                                                                   // Set testingFlare to false for good measure 
        }
    }

    // This function was initially used for randomly choosing an attack.
    // But still keeping this funtion here for legacy sake, reference, and options. 
    void RandomAttackRangeChecker(Vector3 target)
    {
        float distanceToTarget = Vector3.Distance(target, thisRB.transform.position);

        if (distanceToTarget < poundRange && cooldown == false && attacking == false)
        {
            AttackChanger(); 
            if (testingGrdPnd)
            {
                lastCo = StartCoroutine(GroundPound());
                testingGrdPnd = false;
            }
            if (testingFlare)
            {
                lastCo = StartCoroutine(Flare());
                testingFlare = false;
            }
        }
    }

    // Ditto with the above function 
    void AttackChanger()
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 0)
        {
            testingGrdPnd = true; 
        }
        if (randNum == 1)
        {
            testingFlare = true;
        }
    }

    // Moves towards the player
    void MoveForward()
    {
        // Move teh transform forward walkSpeed fast 
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, walkSpeed * Time.deltaTime);
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("run") ) // If the animation is playing the run animation... 
        {
            // do nothing (this is necessary)... 
        }
        else // else... 
        {
            anim.SetBool("run", true);  // play the run animation 
        }
    }

    void MoveToTarget(Vector3 target)
    {
        angle = TargetLocationAngle(target);                                            // The angle the boss is to the player 
        float distanceToTarget = Vector3.Distance(target, thisRB.transform.position);   // The distance the boss is from the player 

        if (distanceToTarget > poundRange && cooldown == false && attacking == false)   // If the boss is not within poundRange, is not cooling down, and is not attacking...
        {
            MoveForward();                                                              // call the  MoveForward() function to move towards the player 
        }

        if (testingFlare && cooldown == false && attacking == false)                    // If flare isn't being used, and boss isn't cooling down, and isn't attacking... 
        {
            lastCo = StartCoroutine(Flare());                                           // Start the Flare attack coroutine 
        }
        if (testingGrdPnd && cooldown == false && attacking == false)                   // If ground pound isn't being used, and boss isn't cooling down, and isn't attacking... 
        {
            lastCo = StartCoroutine(GroundPound());                                     // Start the GroundPound attack coroutine 
        }
    }

    //this function rotates the enemy towards the player based on rotationSpeed (largely based on Darrell's function)
    void RotateToTarget(Vector3 target) 
    {
        Vector3 targetDir = target - transform.position; 

        float step = rotSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        newDir.y = 0f;                                                                          //zero out rotation here
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    float TargetLocationAngle(Vector3 target)                                                              //returns the player's angle infront of the enemy. 0 is infront, 180 is the back
    {
        Vector3 targetDir = target - thisRB.transform.position;
        return Mathf.Abs(Vector3.SignedAngle(targetDir, transform.forward, transform.up));
    }

    public void ActiveToggle()  // Use this function to awaken the beast
    {
        isActive = true;        // isActive is set to true 
    }

    public void StartDeath()    // This function is when the boss dies 
    {
        thisRB.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation; // Sets the rigidbody to freeze potision and rotation 
        StopAllCoroutines();            // Stop all coroutines this script is running 
        isActive = false;               // isActive is set to false so no other functions will work 
        isDead = true;                  // isDead is set to true 
        ClearAllAnim();                 // Stop all animations 
        StartCoroutine(Death());        // Start the Death() coroutine 
        song.Stop();                    // Stop the boss music 
        deathSFX.Play();                // Start the death sound effect 
        anim.SetBool("die", true);      // Play the dieing animation 
    }

    public bool IsBossDead()    // Returns the boss' death state (false until HP reaches zero)
    {
        return isDead; 
    }

    void ClearAllAnim() // Stops all animations from playing 
    {
        anim.SetBool("defy", false);
        anim.SetBool("idle", false);
        anim.SetBool("dizzy", false);
        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        anim.SetBool("jump", false);
        anim.SetBool("die", false);
        anim.SetBool("jump_left", false);
        anim.SetBool("jump_right", false);
        anim.SetBool("attack_01", false);
        anim.SetBool("attack_03", false);
        anim.SetBool("attack_02", false);
        anim.SetBool("damage", false);
    }

    IEnumerator GroundPound()   // The ground pound attack 
    {
        attacking = true;                                                           // Set is Attacking to true so functions know that an attack is in progress
        isGrdPnd = true;                                                            // Set isGrdPnd to trun so functions know this attack is being played 
        anim.SetBool("attack_03", true);                                            // Start the attack_03 animation 
        yield return new WaitForSeconds(1.5f);                                      // Wait 1.5 seconds so the animation will play out 
        Instantiate(poundWave, poundHand.transform.position, transform.rotation);   // Instantiate the pounwWave prefab at the poundHand's loc
        yield return new WaitForSeconds(0.3f); // 1.8f end                          // Wait an additiona .3 seconds
        anim.SetBool("attack_03", false);                                           // Stop the attack_03 animation 
        anim.SetBool("idle", true);                                                 // Play the idle animation 
        isGrdPnd = false;                                                           // Set isGrdPnd to false because the attack is done 
        lastCo = null;                                                              // Clear lastCo because this attack isn't being done anymore 
        StartCoroutine(CoolDown());                                                 // Start cooldown
    }

    IEnumerator Flare()         // The Flare attack where it summons fire to track you 
    {
        attacking = true;                       // Set is Attacking to true so functions know that an attack is in progress 
        isFlare = true;                         // Set isGrdPnd to trun so functions know this attack is being played
        anim.SetBool("attack_02", true);        // Start the attack_02 animation (that I HEAVILY modified (albeit poorly)) 
        StartCoroutine(FireSpawn());            // Start spawning fire objects
        yield return new WaitForSeconds(1.7f);  // wait 1.7 seconds so the animation will play out 
        anim.SetBool("attack_02", false);       // Stop the attack_02 animation 
        anim.SetBool("idle", true);             // Play the idle animation 
        isFlare = false;                        // Set isFlare to false because the attack is done 
        lastCo = null;                          // Clear lastCo because this attack isn't being done anymore
        StartCoroutine(CoolDown());             // Start cooldown
    }

    // Spawns a bunch of fire projectiles to track the player 
    // I'm only going to explain once because it's pretty rinse & repeat 

    // The seconds waited in between the instantiations is largely based on the 
    // animation that I modified. I wanted it to be a pretty even spread of fire
    // while the animation was slowed down to it looked pretty cool while also being deadly 
    IEnumerator FireSpawn()
    {
        yield return new WaitForSeconds(0.9f);                              // Wait 0.9 seconds for animation 
        Instantiate(fire, fireHand.transform.position, transform.rotation); // Instantiate the fire prefab 
        flareSFX.Play();                                                    // Play the flare sound effect
        yield return new WaitForSeconds(0.05f);                             // Wait 0.05 seconds for animation & sfx
        flareSFX.Stop();                                                    // Stop the flare sfx
        flareSFX.Play();                                                    // etc, etc, etc, ...
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        flareSFX.Stop();
        flareSFX.Play();
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        flareSFX.Stop();
        flareSFX.Play();
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        flareSFX.Stop();
        flareSFX.Play();
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        flareSFX.Stop();
        flareSFX.Play();
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        flareSFX.Stop();
        flareSFX.Play();
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        flareSFX.Stop();
        flareSFX.Play();
        Instantiate(fire, fireHand.transform.position, transform.rotation);
    }

    // This coroutine is activated after an attack. It gives a delay between attacks. Enemy can move while on cooldown. 
    // This function was largely based on Darrell's 
    IEnumerator CoolDown() 
    {
        cooldown = true;
        attacking = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
        testingFlare = false;
        testingGrdPnd = false;
    }

    // This function is called once boss HP reaches zero 
    IEnumerator Death()
    {
        yield return new WaitForSeconds(4f);        // Wait 4 seconds so the animation and sfx play out 
        Application.LoadLevel("Hunter-WinScreen");  // Load the win screen and bask in your glory 
    }
}
