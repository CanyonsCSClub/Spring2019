/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  06:00 PM by Hunter Goodin
 * File Name:    MoveUpAndDown.cs 
 * Description:  This script is to raise and lower a spotlight over the heal area. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    private bool isUp;          // If true, the obj will move up  
    private bool isDo;          // If true, the obj will move down 
    private Rigidbody rb;       // The rigidbody this script is attached to 
    public float speed = 0.5f;  // The speed at which the object will move 

	void Start ()
    {
        rb = GetComponent<Rigidbody>(); // Set rb to the rigidbody this script is attacehd to 
        isUp = true;                    // Set isUp to true
        isDo = false;                   // Set isDo to false (should already be false but jic)
        StartCoroutine(ChangeDir());    // Start the ChangeDir coroutine 
	}
	
	void Update ()
    {
		if(isUp)                                                              // is isUp is true... 
        {
            rb.transform.Translate(transform.up * Time.deltaTime * speed);    // Move rb up 
        }
        if(isDo)                                                              // is isDo is true... 
        {
            rb.transform.Translate(-transform.up * Time.deltaTime * speed);   // Move rb down 
        }
	}

    IEnumerator ChangeDir() 
    {
        while (0 < 1)                           // Basically a never ending loop... 
        {
            yield return new WaitForSeconds(2); // Wait 2 seconds 
            isUp = !isUp;                       // toggle isUp 
            isDo = !isDo;                       // toggle isDo 
        }
    }
}
