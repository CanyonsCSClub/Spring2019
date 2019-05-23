using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressFTrigger : MonoBehaviour
{
    public bool inside;
    public bool pressedF; 
    public GameObject fText;
    public GameObject healZone;

    void Update ()
    {
        if (inside == true)
        {
            fText.SetActive(true);
            FListener(); 
        }
        else if (inside == false)
        {
            fText.SetActive(false); 
        }
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")
        {
            inside = true;
            FListener(); 
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.name == "Player")
        {
            inside = false; 
        }
        
    }

    private void FListener()
    {
        if (Input.GetKeyDown("f"))
        {
            pressedF = true;
            healZone.SetActive(true);
            Destroy(fText);
            Destroy(gameObject);
        }
    }
}
