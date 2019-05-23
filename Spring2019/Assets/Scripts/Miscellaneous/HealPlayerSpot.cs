using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayerSpot : MonoBehaviour
{
    public bool inside;
    public int hp; 
    private float healRate = 0.1f;
    private float nextHeal;
    GameObject player; 

    void Update ()
    {
        if (inside == true)
        {
            if (Time.time > nextHeal)
            {
                hp += 1;
                nextHeal = Time.time + healRate;
                player.GetComponent<Health>().ChangeHealth(1);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            inside = true;
            player = col.gameObject; 
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            inside = false;
            player = col.gameObject; 
        }
    }
}
