/* 
 * Author: Darrell Wong 
 * Start Date: 5/10/2019
 * last updated: 5/17/2019
 * Description:     scripting for the shooty enemy's projectile
 *               I decided to have the projectile move itself with a transform.position so that I can have a definite projectileSpeed for the prediction shooting to work;
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyEnemyProjectile : MonoBehaviour
{
    private Vector3 startPos;
    public float projectileSpeed;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 2);
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * Time.deltaTime * projectileSpeed;
        //transform.position = Vector3.MoveTowards(startPos, transform.forward, Time.deltaTime * projectileSpeed);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Shield(Clone)")
        {
            Destroy(gameObject); 
        }
        if (col.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
