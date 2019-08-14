/*
 * Programmer:   Hunter Goodin 
 * Date Created: 06/07/2019 @  3:35 PM 
 * Last Updated: 08/13/2019 @  6:40 PM by Hunter Goodin
 * File Name:    UnlockSSystem.cs 
 * Description:  This script will be responsible for unlock system. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{
    public bool isShield;
    public bool isBow; 

    public void UnlockShield()
    {
        isShield = true;
        gameObject.GetComponent<Shield>().enabled = true;
    }

    public void UnlockBow()
    {
        isBow = true;
        gameObject.GetComponent<InstantiateOnPress>().enabled = true; 
    }

    public bool GetShieldStatus()
    {
        return isShield; 
    }

    public bool GetBowStatus()
    {
        return isBow; 
    }
}
