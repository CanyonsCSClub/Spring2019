/*
 * Programmer:   Hunter Goodin 
 * Date Created: 04/19/2019 @ 12:35 PM 
 * Last Updated: 08/13/2019 @ 12:45 PM by Hunter Goodin
 * File Name:    MakeNotChild.cs 
 * Description:  This script will be responsible for every object that starts off as a 
 				 child of another object but we want it to ultimately be parentless. 
 				 This is significant for the health bars for the enemies which are 
 				 initially children of the enemy in the prefab but we want them to be 
 				 parentless on start. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNotChild : MonoBehaviour
{
	// This will be populated with the transform that this script is attached to in-script 
    private Transform itself; 

	void Start ()
    {
    	// Set itself to a reference of the transform this script is attached to 
        itself = GetComponent<Transform>();	
        // Make this object a standalone object with out a parent in the heirarchy 
        itself.parent = null; 
    }
}
