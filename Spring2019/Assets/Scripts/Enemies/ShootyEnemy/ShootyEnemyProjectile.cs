using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyEnemyProjectile : MonoBehaviour {

    private Vector3 startPos;
    public float projectileSpeed;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 5);
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * Time.deltaTime * projectileSpeed;
        //transform.position = Vector3.MoveTowards(startPos, transform.forward, Time.deltaTime * projectileSpeed);
    }
}
