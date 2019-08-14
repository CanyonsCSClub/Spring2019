/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  03:00 PM by Hunter Goodin
 * File Name:    BossArenaController.cs 
 * Description:  This script will be responsible for closing in the boss arena so the player can not
 *               leave the boss fight once it begins to... say... heal... (Mwuahahah). 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaController : MonoBehaviour
{
    public GameObject bossFightTrigger; // To be populated with the object with the BossFightTrigger.cs script attached in-engine 
    public GameObject boss;             // The boss object to be populated in-engine 
    public GameObject arenaWall;        // The wall that spawns so there is no turning back to be populated in-engine (inactive by default) 
    public GameObject bossHealthText;   // The name of the boss (in canvas) to be populated in-engine 
    public GameObject bossHealthBar;    // The boss's health bar (in canvas) to be populated in-engine

    // The boss' evil friends to be populated in-engine 
    public GameObject possy1; 
    public GameObject possy2;
    public GameObject possy3;
    public GameObject possy4;
    public GameObject possy5;
    public GameObject possy6;

    public bool fightBegin; // If true, the fight has begun

    public AudioSource music1;  // The normal music for the level
    public AudioSource music2;  // The wicked melody of the boss fight 
    
	void Update ()
    {
		if (fightBegin == true)   // Once fightBegin = true... 
        {
            boss.GetComponent<BossEnemy>().ActiveToggle();  // activate the boss prefab... 
            ActivateArenaWalls();                           // activate the arena walls... 
            Destroy(bossFightTrigger);                      // aestroy the trigger (we don't need it anymore)
            bossHealthText.SetActive(true);                 // activate the boss' name text 
            bossHealthBar.SetActive(true);                  // activate the boss's health 
            possy1.SetActive(true);                         // activate the boss possy
            possy2.SetActive(true); 
            possy3.SetActive(true);
            possy4.SetActive(true);
            possy5.SetActive(true);
            possy6.SetActive(true);
            music1.enabled = false;                         // turn off the default level music
            music2.enabled = true;                          // turn on the boss fight music
        }   
        fightBegin = false;         // Set fightBegin to false to mitigate lag 
	}

    public void ToggleBossFight()   // a public toggle for fightBegin so it can be accessed by BossFightTrigger.cs 
    {
        fightBegin = true; 
    }

    public void ActivateArenaWalls()    // Put up the arena walls
    {
        arenaWall.SetActive(true);      // Set the arenaWall object to active 
    }
}
