/*
 * Programmer:   Hunter Goodin
 * Date Created: 03/15/2019 @  11:30 PM 
 * Last Updated: 08/13/2019 @  02:35 PM by Hunter Goodin
 * File Name:    Health.cs 
 * Description:  This script is responsible for handling the health system. 
 *               Thomas was having some issues getting this thing running so
 *               I went ahead and did it instead. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Health : MonoBehaviour
{
	public int health = 100;            // The health variable (starts off with 100 health (full health)) 
    public int maxHealth = 100;         // The highest the health can go 
    public Text displayHealth;          // The text that displays the health's value (in canvas) 
    public GameObject healthBarObj;     // The health bar object 
    private Rigidbody rb;               // The rigidbody this script is attacehd to 
    private bool isDead;                // True if the object is dead 
    public bool invincible;             // True if the object is invincible (Gerardo wanted this feature)

    void Start()
    {
        rb = GetComponent<Rigidbody>();         // Set rb = to the rigidbody this script is attached to 
        invincible = false;  //GERARDO EDIT
    }

    void Update()
    {
        if (health > maxHealth)                                         // If health is higher than maxHealth... 
        { health = maxHealth; }                                         // set health = to maxHealth 
        if (health < 0)                                                 // If health is less than 0... 
        { health = 0; }                                                 // set health = to zero 

        healthBarObj.GetComponent<HealthBar>().HealthGetter(health);    // In the healthBarObj, grab the HealthBar.cs script and call the HealthGetter function with the parameter health

        displayHealth.text = "" + health.ToString();                    // Set the health text in the canvas to health's current value

        if (health <= 0)                                            // If health is less than 0... 
        {   
            if (gameObject.name == "Orc")                           // and the object's name is orc... 
            {
                gameObject.GetComponent<BigEnemy>().StartDeath();   // call the object's script's StartDeath() function... 
                Destroy(healthBarObj);                              // destroy the health bar obj... 
                gameObject.GetComponent<Health>().enabled = false;  // and disable that object's Health.cs script 
            }
            if (gameObject.name == "ShootyEnemy")                   // ^^ same as above 
            {
                gameObject.GetComponent<ShootyEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "MushRed")                      // ^^ same as above 
            {
                gameObject.GetComponent<BasicEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "MushGreen")                   // ^^ same as above 
            {
                gameObject.GetComponent<BasicEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "MushBlue")                   // ^^ same as above 
            {
                gameObject.GetComponent<BasicEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "Orcutoryx The Scourge")      // ^^ same as above 
            {
                gameObject.GetComponent<BossEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "Player")                        // If the object is the player... 
            {
                gameObject.GetComponent<CoreMovement>().MakeDead(); // call the player's core movement behavioral script and call the MakeDead() function...
                StartCoroutine(LoadDeathMenu());                    // then call the LoadDeathMenu() coroutine to load the death menu 
            }
        }
    }

    public void ChangeHealth(int adj)   // To edit health 
    {
        if (!invincible)                                        //GERARDO EDIT
        {
            health = health + adj;      // set health = to the value passed 
        }
    }

    IEnumerator LoadDeathMenu()
    {
        yield return new WaitForSeconds(2f);            // Wait two second for the animation to play out 
        Application.LoadLevel("Hunter-DeathScreen");    // Load the death screen 
    }
}