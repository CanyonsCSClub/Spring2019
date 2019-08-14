/*
 * Programmer:   Hunter Goodin 
 * Date Created: 06/07/2019 @  6:00 PM 
 * Last Updated: 08/13/2019 @  6:40 PM by Hunter Goodin
 * File Name:    SwordDamage.cs 
 * Description:  This script will be responsible for making the sword hurt bad guys pretty bad. 
                 I used lists to keep track of everything the sword is touching. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public List<Collider> TriggerList = new List<Collider>();
    private int damage = 50;
    private int bossDam = 10;
    private Vector3 direction;

    void OnTriggerEnter(Collider col)
    {
        direction = (col.transform.position - GameObject.Find("Player").transform.position).normalized;
        if (col.gameObject.tag == "Enemy" && CheckList(col.gameObject.name) == false)
        {
            if (!TriggerList.Contains(col))
            {
                TriggerList.Add(col);
            }

            col.gameObject.GetComponent<Health>().ChangeHealth(-damage);

            if (col.gameObject.name == "Orc")
            {
                col.gameObject.GetComponent<BigEnemy>().StartHit();
            }
            if (col.gameObject.name == "ShootyEnemy")
            {
                col.gameObject.GetComponent<ShootyEnemy>().StartHit();
            }
            if (col.gameObject.name == "MushRed" || col.gameObject.name == "MushGreen" || col.gameObject.name == "MushBlue")
            {
                col.gameObject.GetComponent<BasicEnemy>().StartHit();
            }

            col.GetComponent<Rigidbody>().AddForce(direction * 500);
        }
        if (col.gameObject.tag == "Boss" && CheckList(col.gameObject.name) == false)
        {
            if (!TriggerList.Contains(col))
            {
                TriggerList.Add(col);
            }

            col.gameObject.GetComponent<Health>().ChangeHealth(-bossDam);
        }
    }

    public void CleanList()
    {
        TriggerList = new List<Collider>();
    }

    private bool CheckList(string objName)
    {
        bool check = false;
        for (int i = 0; i < TriggerList.Count; i++)
        {
            if (TriggerList[i].name == objName)
            {
                check = true;
                i = TriggerList.Count;
            }
            else
            {
                check = false;
            }
        }
        return check;
    }
}
