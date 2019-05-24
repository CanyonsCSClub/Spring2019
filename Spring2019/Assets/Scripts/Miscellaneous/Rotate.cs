using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody rb;
    public float rotSpd;

    public bool isUp;
    public bool isLeft;
    public bool isRight;
    public bool isDown; 

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        if (isUp)
        {
            rb.transform.Rotate(Vector3.up * Time.deltaTime * rotSpd);
        }
        if (isDown)
        {
            rb.transform.Rotate(-Vector3.up * Time.deltaTime * rotSpd);
        }
        if (isLeft)
        {
            rb.transform.Rotate(-Vector3.right * Time.deltaTime * rotSpd);
        }
        if (isRight)
        {
            rb.transform.Rotate(Vector3.right * Time.deltaTime * rotSpd);
        }
    }
}
