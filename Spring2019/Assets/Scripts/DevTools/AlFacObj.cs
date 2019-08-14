/*
 * Programmer:   Hunter Goodin 
 * Date Created: 05/24/2019 @  8:15 PM 
 * Last Updated: 08/13/2019 @  2:20 PM by Hunter Goodin
 * File Name:    CreditsMusicControl.cs 
 * Description:  This script is a tool to help in the dev process. 
                 It turns the object this script is attached to
                 to face whatever object is populated by objToTrack. 
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlFacObj : MonoBehaviour
{
    // This will be populated by the transform this script is attached to in-script
    private Transform itself;
    // This will be populated by the transform of the object we want to face in-engine 
    public Transform objToTrack;

    void Start()
    {
        // On start, store the transform that this script is attacehd to in "itself" 
        itself = GetComponent<Transform>(); 
    }

	void Update()
    { 
        // Every frame, look at the object we want to track 
        itself.LookAt(objToTrack);
    }
}
