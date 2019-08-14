/*
 * Programmer:   Hunter Goodin 
 * Date Created: 06/07/2019 @  3:35 PM 
 * Last Updated: 08/13/2019 @  6:40 PM by Hunter Goodin
 * File Name:    UnlockText.cs 
 * Description:  This script will be responsible for making text appear after the player obtains an item. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockText : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 1.5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 1.5f); 
    }

    void Update ()
    {
        rb.transform.Translate(transform.up * Time.deltaTime * speed);
    }
}
