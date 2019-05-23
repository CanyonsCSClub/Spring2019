using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    private bool isUp;
    private bool isDo;
    private Rigidbody rb;
    public float speed = 0.5f; 

	void Start ()
    {
        rb = GetComponent<Rigidbody>(); 
        isUp = true;
        isDo = false;
        StartCoroutine(ChangeDir()); 
	}
	
	void Update ()
    {
		if(isUp)
        {
            rb.transform.Translate(transform.up * Time.deltaTime * speed);
        }
        if(isDo)
        {
            rb.transform.Translate(-transform.up * Time.deltaTime * speed);
        }
	}

    IEnumerator ChangeDir()
    {
        while (0 < 1)
        {
            yield return new WaitForSeconds(2);
            isUp = !isUp;
            isDo = !isDo;
        }
    }
}
