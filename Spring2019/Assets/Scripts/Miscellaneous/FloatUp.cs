using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUp : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 0.3f; 

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 4); 
    }
	
	void Update ()
    {
        rb.transform.Translate(transform.up * Time.deltaTime * speed);
        rb.transform.Translate(transform.forward * Time.deltaTime * speed);
        rb.transform.Rotate(Vector3.up * Time.deltaTime * 100);
    }
}
