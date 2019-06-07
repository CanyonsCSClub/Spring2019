using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    public float speed = 4;
    public Vector3 playerPos; 
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>(); 
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
        Destroy(gameObject, 3f); 
	}

	void Update ()
    {
        // transform.Translate(transform.forward * Time.deltaTime * speed);
        //  rb.AddForce(transform.forward * 500);
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
        transform.position = Vector3.MoveTowards(gameObject.GetComponent<Transform>().position, playerPos, Time.deltaTime * speed);

        if (GameObject.Find("Orcutoryx The Scourge").GetComponent<BossEnemy>().IsBossDead())
        {
            Destroy(gameObject); 
        }
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
