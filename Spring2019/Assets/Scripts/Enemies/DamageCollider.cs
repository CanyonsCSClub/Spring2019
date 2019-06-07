#pragma strict
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public int damage;
    public List<Collider> TriggerList = new List<Collider>();
    public List<Collider> cleanList = new List<Collider>();

    public bool dam; 

    private void Update()
    {
        CleanList(); 
    }

    private void OnTriggerEnter(Collider col)
    {
        if(!TriggerList.Contains(col))
        {
             TriggerList.Add(col);
        }
        
        if(col.gameObject.name == "Player" && CheckList("Shield(Clone)") == false)
        {
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
