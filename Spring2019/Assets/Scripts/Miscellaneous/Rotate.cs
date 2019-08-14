/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  06:20 PM by Hunter Goodin
 * File Name:    Rotate.cs 
 * Description:  This script will rotate whatever rb this is attached to. 
 *               Used for the power-ups. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody rb;   // The rigidbody this script is attached to to be populated in-script 
    public float rotSpd;    // The speed at which rb will rotate to be populated in-engine 

    public bool isUp;       // True if I want the rb to rotate pos on the Y-axis  
    public bool isLeft;     // True if I want the rb to rotate pos on the X-axis  
    public bool isRight;    // True if I want the rb to rotate neg on the X-axis  
    public bool isDown;     // True if I want the rb to rotate neg on the Y-axis  

	void Start ()
    {
        rb = GetComponent<Rigidbody>(); // Set the rb to the rigidbody this script is attached to 
    }
	
	void Update ()
    {
        if (isUp)                                                           // If isUp is true... 
        {
            rb.transform.Rotate(Vector3.up * Time.deltaTime * rotSpd);      // rotate rb pos on the Y-axis 
        }
        if (isDown)                                                         // If isDown is true... 
        {
            rb.transform.Rotate(-Vector3.up * Time.deltaTime * rotSpd);     // rotate rb pos on the X-axis 
        }
        if (isLeft)                                                         // If isLeft is true... 
        {
            rb.transform.Rotate(-Vector3.right * Time.deltaTime * rotSpd);  // rotate rb neg on the X-axis 
        }
        if (isRight)                                                        // If isDown is true... 
        {
            rb.transform.Rotate(Vector3.right * Time.deltaTime * rotSpd);   // rotate rb neg on the Y-axis 
        }
    }
}
