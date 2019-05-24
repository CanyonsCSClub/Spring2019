using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public float howLong = 5;

	void Start () {
        Destroy(gameObject, howLong);
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

}
