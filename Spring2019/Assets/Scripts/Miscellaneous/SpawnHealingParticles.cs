using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealingParticles : MonoBehaviour
{
    public GameObject particle;
    public float spawnRad; 
    private Transform tr;
    private Vector3 spawnLoc;
    private Quaternion spawnRot;
    private float spawnRate = 1;
    private float nextSpawn;
    private float randNumX;
    private float randNumY;
    private GameObject clone; 

    void Start ()
    {
        tr = GetComponent<Transform>();
        spawnLoc = GetComponent<Transform>().position; 
        spawnRot = new Quaternion(0, 0, 0, 0); 
	}
	
	void Update ()
    {
        randNumX = Random.Range(-spawnRad, spawnRad);
        randNumY = Random.Range(-spawnRad, spawnRad);

        spawnLoc.x = GetComponent<Transform>().position.x + randNumX;
        spawnLoc.z = GetComponent<Transform>().position.z + randNumY; 

		if (Time.time > nextSpawn)
        {
            clone = Instantiate(particle, spawnLoc, spawnRot);
            clone.transform.parent = tr;
            nextSpawn = Time.time + spawnRate;
        }
    }
}
