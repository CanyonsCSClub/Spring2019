using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    public GameObject shieldInstance;
    public GameObject Player;
    GameObject clone;
    Vector3 shieldposition;
    Quaternion shieldrotation = Quaternion.Euler(0,90,0);

    bool isNorth = false;
    bool isSouth = false;
    bool isWest = false;
    bool isEast = false;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("a");
            isNorth = false;
            isSouth = false;
            isEast = false;
            isWest = true;

        }
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("s");
            isNorth = false;
            isSouth = true;
            isEast = false;
            isWest = false;

        }
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("w");
            isNorth = true;
            isSouth = false;
            isEast = false;
            isWest = false;

        }
        if (Input.GetKeyDown("d"))
        {
            Debug.Log("d");
            isNorth = false;
            isSouth = false;
            isEast = true;
            isWest = false;
        }

        if (Input.GetButtonDown("Block"))                             //if the block button has been pressed(c)
        {
            float shieldy = Player.transform.position.y;             
            float shieldx = Player.transform.position.x;
            float shieldz = Player.transform.position.z; ;    
            shieldposition.Set(shieldx, shieldy, shieldz + .9f); //default shield position in case player hasnt moved is in front
            if (isNorth)
            {
                shieldposition.Set(shieldx, shieldy, shieldz + .9f);
                shieldrotation = Quaternion.Euler(0, 90, 0);
            }
            if (isEast)
            {
                shieldposition.Set(shieldx+0.9f, shieldy, shieldz);
                shieldrotation = Quaternion.Euler(0, 0, 0);
            }
            if (isSouth)
            {
                shieldposition.Set(shieldx, shieldy, shieldz - .9f);
                shieldrotation = Quaternion.Euler(0, 90, 0);
            }
            if (isWest)
            {
                shieldposition.Set(shieldx-0.9f, shieldy, shieldz);
                shieldrotation = Quaternion.Euler(0, 0, 0);
            }
            clone = Instantiate(shieldInstance, shieldposition, shieldrotation); 
            clone.transform.parent = Player.transform;  

        }
        if (Input.GetButtonUp("Block"))
        {
            Destroy(clone);                                                     //when button is released shield goes away
        }

    }
}
