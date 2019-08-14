/*
 * Programmer:   Hunter Goodin 
 * Date Created: 05/24/2019 @  8:15 PM 
 * Last Updated: 08/13/2019 @  2:20 PM by Hunter Goodin
 * File Name:    EscListener.cs 
 * Description:  This script will leave the credits menu and load
                 the main menu when the escape key is clicked. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscListener : MonoBehaviour
{
	void Update ()										// Every frame... 
    {
		if (Input.GetKey(KeyCode.Escape))				// if the excape key is pressed... 
        {
            Application.LoadLevel("Madian-MainMenu");	// Go back to main menu 
        }
    }
}
