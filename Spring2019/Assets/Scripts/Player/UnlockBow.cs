/*
 * Programmer:   Hunter Goodin 
 * Date Created: 06/07/2019 @  3:35 PM 
 * Last Updated: 08/13/2019 @  6:40 PM by Hunter Goodin
 * File Name:    UnlockBow.cs 
 * Description:  This script will be responsible for unlocking the bow. 
 */

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
