#pragma strict
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public int damage;
    // public GameObject baddy; 
    public List<Collider> TriggerList = new List<Collider>();
    public List<Collider> cleanList = new List<Collider>();

    public bool dam;

    public Vector3 direction;

    private void Update()
    {
        CleanList(); 
    }

    private void OnTriggerEnter(Collider col)
    {
        //direction = (baddy.transform.position - col.transform.position);
        direction = (col.transform.position - transform.position).normalized;

        if (!TriggerList.Contains(col))
        {
             TriggerList.Add(col);
        }
        
        if(col.gameObject.name == "Player" && CheckList("Shield(Clone)") == false)
        {
            col.gameObject.GetComponent<CoreMovement>().PushPlayer(direction);
            // col.GetComponent<Rigidbody>().AddForce(direction * 25);
            col.gameObject.GetComponent<Health>().ChangeHealth(-damage);
            dam = true; 
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (TriggerList.Contains(col))
        {
            TriggerList.Remove(col);
        }
    }

    public void EmptyList()
    {
        for (int i = 0; i < TriggerList.Count; i ++)
        {
            TriggerList.Remove(TriggerList[i]); 
        }
        gameObject.SetActive(false); 
    }

    public void CleanList()
    {
        TriggerList = new List<Collider>();
    }

    private bool CheckList(string objName)
    {
        bool check = false; 
        for(int i = 0; i < TriggerList.Count; i ++)
        {
            if ( TriggerList[i].name == objName)
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
