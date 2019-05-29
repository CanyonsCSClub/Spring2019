using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShield : MonoBehaviour
{
    public GameObject textObj;
    public GameObject shieldObj; 

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.GetComponent<UnlockSystem>().UnlockShield();
            col.GetComponent<CoreMovement>().HasShieldTog();
            shieldObj = GameObject.Find("Shield01Free");
            shieldObj.GetComponent<Renderer>().enabled = true; 
            Instantiate(textObj, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
        }
    }
}
