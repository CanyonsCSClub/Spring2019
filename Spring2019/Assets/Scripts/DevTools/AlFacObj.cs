using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlFacObj : MonoBehaviour
{
    private Transform itself; 
    public Transform objToTrack; 

    void Start()
    {
        itself = GetComponent<Transform>(); 
    }

	void Update()
    {
        itself.LookAt(objToTrack);
    }
}
