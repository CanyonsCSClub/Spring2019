using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float barVal = 1f;
    public Transform bar;

    void Start ()
    {
        bar.localScale = new Vector3(barVal, 1f); 
	}
	
	void Update ()
    {
        bar.localScale = new Vector3(barVal, 1f);
    }

    public void HealthGetter(int health)
    {
        barVal = health / 100f; 
    }
}
