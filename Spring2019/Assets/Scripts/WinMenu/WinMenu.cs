/*
 * Programmer:   Hunter Goodin 
 * Date Created: 06/07/2019 @  3:35 PM 
 * Last Updated: 08/13/2019 @  6:40 PM by Hunter Goodin
 * File Name:    WinMenu.cs 
 * Description:  This script will be responsible for the win menu. 
 *                Pretty similar to the death menu which I already detailed in that script. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public AudioSource sound; 

    public void OnlyButton()
    {
        sound.enabled = true;
        StartCoroutine(RollCredits()); 
    }

    IEnumerator RollCredits()
    {
        yield return new WaitForSeconds(1.3f);
        Application.LoadLevel("Madian-MainMenu");
    }
}
