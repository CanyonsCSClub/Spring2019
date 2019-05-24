using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float howLong = 2;
    private int damage = 25; 

	void Start ()
    {
        Destroy(gameObject, howLong);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Health>().ChangeHealth(-damage);
            Destroy(gameObject); 
        }
    }
}
