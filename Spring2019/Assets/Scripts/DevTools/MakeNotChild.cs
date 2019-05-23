using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNotChild : MonoBehaviour
{
    private Transform itself; 

	void Start ()
    {
        itself = GetComponent<Transform>();
        itself.parent = null; 
    }
}
