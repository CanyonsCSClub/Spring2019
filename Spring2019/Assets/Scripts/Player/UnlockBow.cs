using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBow : MonoBehaviour
{
    public GameObject textObj;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.GetComponent<UnlockSystem>().UnlockBow();
            Instantiate(textObj, transform.position, new Quaternion(0, 0, 0, 0)); 
            Destroy(gameObject); 
        }
    }
}
