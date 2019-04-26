/*
 * Programmer:   Hunter Goodin 
 * Date Created: 04/19/2019 @ 12:35 PM 
 * Last Updated: 03/12/2019 @ 12:45 PM by Hunter Goodin
 * File Name:    MeshVisToggle.cs 
 * Description:  This script will make all child objects of the object it's attached to invisible. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVisToggle : MonoBehaviour
{
    public bool visable;
    public Renderer[] rendArr = new Renderer[0];

    void Start()
    {
        rendArr = GetComponentsInChildren<Renderer>();
        ArrLoop();
    }

    private void Update()
    {
        ArrLoop();
    }

    void ArrLoop()
    {
        if (visable)
        {
            for (int i = 0; i < rendArr.Length; i++)
            {
                rendArr[i].enabled = true;
            }
        }
        if (!visable)
        {
            for (int i = 0; i < rendArr.Length; i++)
            {
                rendArr[i].enabled = false;
            }
        }
    }
}