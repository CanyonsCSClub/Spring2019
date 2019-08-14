/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  06:00 PM by Hunter Goodin
 * File Name:    FloatUp.cs 
 * Description:  This script is to heal the player while the player is within this obj 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayerSpot : MonoBehaviour
{
    public bool inside;             // If is true if the player is inside 
    private float healRate = 0.1f;  // The rate at which the player is healed 
    private float nextHeal;         // The next time the player will be healed 
    GameObject player;              // The obj of the player to be populated in-script 

    void Update ()
    {
        if (inside == true)                                     // if inside = true... 
        {
            if (Time.time > nextHeal)                           // and Time.time is greater than nextHeal... 
            {
                nextHeal = Time.time + healRate;                // set nextHeal = to Time.time + healRate... 
                player.GetComponent<Health>().ChangeHealth(1);  // and increase the player's health by 1 
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")     // If the obj colliding with this obj is the player...
        {
            inside = true;                      // set inside = to true...
            player = col.gameObject;            // set the player to the player obj 
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")    // If the obj colliding with this obj is the player... 
        {
            inside = false;                     // set inside = to false... 
            player = col.gameObject;            // set the player to the player obj (redundant but jic) 
        }
    }
}
