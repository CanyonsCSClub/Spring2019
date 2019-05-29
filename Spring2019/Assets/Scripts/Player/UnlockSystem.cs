using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{
    public bool isShield;
    public bool isBow; 

    public void UnlockShield()
    {
        isShield = true;
        gameObject.GetComponent<Shield>().enabled = true;
    }

    public void UnlockBow()
    {
        isBow = true;
        gameObject.GetComponent<InstantiateOnPress>().enabled = true; 
    }

    public bool GetShieldStatus()
    {
        return isShield; 
    }

    public bool GetBowStatus()
    {
        return isBow; 
    }

    // playerPos.gameObject.GetComponent<UnlockSystem>().GetShieldStatus()
}
