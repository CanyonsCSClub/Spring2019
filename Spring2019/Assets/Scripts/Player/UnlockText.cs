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
