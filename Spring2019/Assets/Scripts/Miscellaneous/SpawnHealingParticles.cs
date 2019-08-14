/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  06:00 PM by Hunter Goodin
 * File Name:    SpawnHealingParticles.cs 
 * Description:  This script will be responsible for the cosmetic floaties around the healing area. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealingParticles : MonoBehaviour
{
    public GameObject particle;     // The prefab to be spawned 
    public float spawnRad;          // The radius the floaties will be spawned (technically a square)
    private Transform tr;           // The transform that this script is attached to 
    private Vector3 spawnLoc;       // The location of a floaty being spawned 
    private Quaternion spawnRot;    // The rotation of a floaty being spawned 
    private float spawnRate = 1;    // Spawn rate 
    private float nextSpawn;        // The next time a floaty should be spawned 
    private float randNumX;         // Random x val 
    private float randNumY;         // Random y val 
    private GameObject clone;       // A reference to a particular floaty 

    void Start ()
    {
        tr = GetComponent<Transform>();                 // Set the tr to the transform this script is attached to 
        spawnLoc = GetComponent<Transform>().position;  // Set spawnLoc to tr's position 
        spawnRot = new Quaternion(0, 0, 0, 0);          // Set spawnRot to 0,0,0,0
	}
	
	void Update ()
    {
        randNumX = Random.Range(-spawnRad, spawnRad);                   // Set randNumX to a random number within the spawnRad
        randNumY = Random.Range(-spawnRad, spawnRad);                   // Set randNumY to a random number within the spawnRad

        spawnLoc.x = GetComponent<Transform>().position.x + randNumX;   // Set spawnLoc's x val to the position of the tr + randNumX 
        spawnLoc.z = GetComponent<Transform>().position.z + randNumY;   // Set spawnLoc's y val to the position of the tr + randNumY 

		if (Time.time > nextSpawn)                                // If Time.time is greater than nextSpawn... 
        {
            clone = Instantiate(particle, spawnLoc, spawnRot);    // set clone = to and instantiate a particle within the spawnLoc... 
            clone.transform.parent = tr;                          // set the clone's parent = to tr 
            nextSpawn = Time.time + spawnRate;                    // Set nextSpawn to Time.time + spawnRate 
        }
    }
}
