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

    // atk stuff 
    public float poundRange;
    public float fireRange;
    public bool cooldown;
    private Coroutine lastCo = null;
    public bool isGrdPnd;
    public bool isFlare;
    public bool attacking;
    public int cooldownTime;
    public GameObject fireHand;
    public GameObject fire;
    public GameObject poundHand;
    public GameObject poundWave; 

    public bool testingFlare;
    public bool testingGrdPnd;

    public bool isDead;

    private void Start()
    {
        thisRB = GetComponent<Rigidbody>();
        playerObj = GameObject.Find("Player");
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 
    }

    void Update ()
    {
        if (isActive)
        {
            thisPos = thisRB.position;
            playerPos = playerRB.position;
            MoveToTarget(playerPos);
            //if (!cooldown)
            //{
            //    RotateToTarget(playerPos);
            //}
            if (!attacking)
            {
                RotateToTarget(playerPos);
            }
            AttackRangeChecker(playerPos); 
        }
    }

    void AttackRangeChecker(Vector3 target)
    {
        float distanceToTarget = Vector3.Distance(target, thisRB.transform.position);

        if (distanceToTarget < poundRange && cooldown == false && attacking == false && !isDead)
        {
            lastCo = StartCoroutine(GroundPound());
            testingGrdPnd = false;
        }
        if (distanceToTarget < fireRange && cooldown == false && attacking == false && !isDead)
        {
            lastCo = StartCoroutine(Flare());
            testingFlare = false;
        }
    }

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

    void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, walkSpeed * Time.deltaTime);
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("run") )
        {
            // do nothing
        }
        else
        {
            anim.SetBool("run", true);
        }
    }

    void MoveToTarget(Vector3 target)
    {
        angle = TargetLocationAngle(target);
        float distanceToTarget = Vector3.Distance(target, thisRB.transform.position); ;

        if (distanceToTarget > poundRange && cooldown == false && attacking == false) 
        {
            MoveForward(); 
        }

        if (testingFlare && cooldown == false && attacking == false)
        {
            lastCo = StartCoroutine(Flare());
        }
        if (testingGrdPnd && cooldown == false && attacking == false)
        {
            lastCo = StartCoroutine(GroundPound());
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

    float TargetLocationAngle(Vector3 target)                                                              //returns the player's angle infront of the enemy. 0 is infront, 180 is the back
    {
        Vector3 targetDir = target - thisRB.transform.position;
        return Mathf.Abs(Vector3.SignedAngle(targetDir, transform.forward, transform.up));
    }

    public void ActiveToggle()
    {
        isActive = true; 
    }

    public void StartDeath()
    {
        StopCoroutine(lastCo); 
        isActive = false;
        isDead = true; 
        ClearAllAnim(); 
        anim.SetBool("die", true);
        Destroy(gameObject, 5);
    }

    public bool IsBossDead()
    {
        return isDead; 
    }

    void ClearAllAnim()
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

    IEnumerator GroundPound()
    {
        attacking = true;
        isGrdPnd = true; 
        anim.SetBool("attack_03", true);
        yield return new WaitForSeconds(1.5f);
        Instantiate(poundWave, poundHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.3f); // 1.8f end
        anim.SetBool("attack_03", false);
        anim.SetBool("idle", true);
        isGrdPnd = false; 
        lastCo = null;
        StartCoroutine(CoolDown());
    }

    IEnumerator Flare()
    {
        attacking = true;
        isFlare = true;
        anim.SetBool("attack_02", true);
        StartCoroutine(FireSpawn()); 
        yield return new WaitForSeconds(1.7f);
        anim.SetBool("attack_02", false);
        anim.SetBool("idle", true);
        isFlare = false;
        lastCo = null;
        StartCoroutine(CoolDown());
    }

    IEnumerator FireSpawn()
    {
        yield return new WaitForSeconds(0.9f); 
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        Instantiate(fire, fireHand.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        Instantiate(fire, fireHand.transform.position, transform.rotation);
    }

    IEnumerator CoolDown()                                      //this coroutine is activated after an attack. It gives a delay between attacks. Enemy can move while on cooldown
    {
        cooldown = true;
        attacking = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
        testingFlare = false;
        testingGrdPnd = false;
    }
}
