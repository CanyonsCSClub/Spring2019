/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  06:00 PM by Hunter Goodin
 * File Name:    FloatUp.cs 
 * Description:  This script is for the particles spawned by the healing tile will float up. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUp : MonoBehaviour
{
    private Rigidbody rb;       // To be ppoulated with the rigidbody this script is attached to in-script 
    public float speed = 0.3f;  // The speed of the obj 

	void Start ()
    {
        rb = GetComponent<Rigidbody>(); // Set rb to the rigidbody this script is attacehd to 
        Destroy(gameObject, 4);         // Destroy this obj after 4 seconds 
    }
	
	void Update ()
    {
        rb.transform.Translate(transform.up * Time.deltaTime * speed);      // Move this obj up speed fast 
        rb.transform.Translate(transform.forward * Time.deltaTime * speed); // Move this obj forward speed fast (so it'll spin a little)
        rb.transform.Rotate(Vector3.up * Time.deltaTime * 100);             // Rotate this obj (so it will spin a little) 
    }
}
