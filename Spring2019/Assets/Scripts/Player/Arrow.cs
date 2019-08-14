/*
 * Programmer:   Hunter Goodin 
 * Date Created: 06/06/2019 @  6:00 PM 
 * Last Updated: 08/13/2019 @  6:30 PM by Hunter Goodin
 * File Name:    Arrow.cs 
 * Description:  This script will be responsible for making sure the arrows do damage 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int damage = 25;    // How much damage the arrow does  
    private int bossDam = 5;    // How much damage the arrow does to the boss 
    private Vector3 direction;  // The direction of the enemy's bounce back after being hit with the arrow 

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Walls")  // If this obj hits a wall... 
        {
            Destroy(gameObject);            // destroy this obj 
        }
        if (col.gameObject.tag == "Enemy")                                                                  // If this obj hits an enemy... 
        {
            direction = (col.transform.position - GameObject.Find("Player").transform.position).normalized; // set the trajectory of the bounce  
            col.gameObject.GetComponent<Health>().ChangeHealth(-damage);                                    // change the enemy's health 

            if(col.gameObject.name == "Orc")                            // If this obj hit an orc... 
            { col.gameObject.GetComponent<BigEnemy>().StartHit(); }     // call the enemy's StartHit() function 
            if (col.gameObject.name == "ShootyEnemy")                   // etc, etc, etc... 
            { col.gameObject.GetComponent<ShootyEnemy>().StartHit(); }
            if (col.gameObject.name == "MushRed")
            { col.gameObject.GetComponent<BasicEnemy>().StartHit(); }
            if (col.gameObject.name == "MushGreen")
            { col.gameObject.GetComponent<BasicEnemy>().StartHit(); }
            if (col.gameObject.name == "MushBlue")
            { col.gameObject.GetComponent<BasicEnemy>().StartHit(); }


            col.GetComponent<Rigidbody>().AddForce(direction * 500);    // Bounce the enemy back a little in the aforementioned trajectory 
            Destroy(gameObject);                                        // Destroy this obj 
        }
        if (col.gameObject.tag == "Boss")   
        {
            direction = (col.transform.position - GameObject.Find("Player").transform.position).normalized;
            col.gameObject.GetComponent<Health>().ChangeHealth(-bossDam);

            Destroy(gameObject);
        }
    }
}
