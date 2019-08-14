/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  03:00 PM by Hunter Goodin
 * File Name:    BossFightToggle.cs 
 * Description:  This script is pretty simple. When the player walks through this
 *				 trigger, the boss fight will begin. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
	// The obj with the BossArenaController.cs script attached to be populated in-engine 
    public GameObject bossArenaController; 	

	// When the player is no longer colliding with this object... 
    private void OnTriggerExit(Collider col)	
    {
    	// call the ToggleBossFight() function of the BossArenaController script attached to BossArenaController 
        bossArenaController.GetComponent<BossArenaController>().ToggleBossFight();	
    }
}
