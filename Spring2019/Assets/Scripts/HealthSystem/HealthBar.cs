/*
 * Programmer:   Hunter Goodin
 * Date Created: 03/15/2019 @  11:30 PM 
 * Last Updated: 08/13/2019 @  02:35 PM by Hunter Goodin
 * File Name:    HealthBar.cs 
 * Description:  This script is responsible for displaying the health of the object with a health bar. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float barVal = 1f;  // The size of the bar 
    public Transform bar;       // The bar's transform to be populated in-engine 

    void Start ()
    {
        bar.localScale = new Vector3(barVal, 1f); // Make sure the bar's size is set accurately
	}
	
	void Update ()
    {
        bar.localScale = new Vector3(barVal, 1f); // Make sure the bar's value is set accurately
    }

    public void HealthGetter(int health) // To be called from the Health.cs script 
    {
        barVal = health / 100f;     // Change the size of the bar to the current health and divide by 100 to get a percentage for the bar's size
    }
}
