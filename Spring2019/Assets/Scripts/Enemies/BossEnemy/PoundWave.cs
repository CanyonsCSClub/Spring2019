/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  04:35 PM by Hunter Goodin
 * File Name:    PoundWave.cs 
 * Description:  This script will be responsible for handling the behavior 
 *               of the pound wave spawned by the boss' ground pound attack
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoundWave : MonoBehaviour
{
    private float incriment = 0.05f; // The incriment we will be expanding the wave by every frame
	void Start ()
    {
        Destroy(gameObject, 0.9f);  // Destroy this obj in 0.9 seconds
	}

	void Update ()
    {
        gameObject.transform.localScale += new Vector3(incriment, incriment, incriment);     // Scale this obj up by incriment size

        if (GameObject.Find("Orcutoryx The Scourge").GetComponent<BossEnemy>().IsBossDead()) // If the boss is dead... 
        {
            Destroy(gameObject);                                                             // Destroy this obj 
        }
    }
}
