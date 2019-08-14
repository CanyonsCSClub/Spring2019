/*
 * Programmer:   Hunter Goodin 
 * Date Created: 04/19/2019 @ 12:35 PM 
 * Last Updated: 08/13/2019 @ 12:45 PM by Hunter Goodin
 * File Name:    MeshVisToggle.cs 
 * Description:  This script will make all child objects of the object it's attached to invisible. 
                 This is significant in having all the invisible collision around the map. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVisToggle : MonoBehaviour
{
    // If this bool is false, all child objects will be invisible (to be toggled in-engine) 
    public bool visable;
    // A rederer array to be populated with every child of the object this script is attached to in-script 
    public Renderer[] rendArr = new Renderer[0];

    void Start()
    {
        // Make rendArr equal to every child of the object this script is attacehd to 
        rendArr = GetComponentsInChildren<Renderer>(); 
        ArrLoop(); // Make every child invisible 
    }

    private void Update()
    {
        ArrLoop();
    }

    void ArrLoop()
    {
        if (visable)                                    // If visable is true... 
        {
            for (int i = 0; i < rendArr.Length; i++)    // loop through ever element in rendArr...
            {
                rendArr[i].enabled = true;              // make them visable 
            }
        }
        if (!visable)                                   // If visable is false... 
        {
            for (int i = 0; i < rendArr.Length; i++)    // loop through every element in rendArr... 
            {
                rendArr[i].enabled = false;             // make them invisible 
            }       
        }
    }
}