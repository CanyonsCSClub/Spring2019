using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shieldInstance;
    public GameObject Player;
    GameObject clone;
    public Vector3 shieldposition;
    public Quaternion shieldrotation = Quaternion.Euler(0,90,0);

    bool isNorth = false;
    bool isSouth = false;
    bool isWest = false;
    bool isEast = false;

    public bool N;
    public bool S;
    public bool E;
    public bool W;
    public bool NW;
    public bool SW;
    public bool NE;
    public bool SE;

    public bool isWorking; 

    public Transform NGO;
    public Transform SGO;
    public Transform EGO;
    public Transform WGO;
    public Transform NWGO;
    public Transform SWGO;
    public Transform NEGO;
    public Transform SEGO;

    // Not the optimal way the shield animations should be handled (partically in the core movement script) but I didn't 
    // want to change too much of the script that was already there. 
    // Also made it to where when the player is using the shield, the player can not move. It was causing too many bugs. 
    // (Hunter) 
    void Update ()
    {
        shieldposition.Set(SGO.position.x, SGO.position.y, SGO.position.z); //default shield position in case player hasnt moved is in front

        if (NW)
        {
            shieldposition.Set(NWGO.position.x, NWGO.position.y + 0.7f, NWGO.position.z);
            shieldrotation = Quaternion.Euler(0, 45, 0);
        }
        if (NE)
        {
            shieldposition.Set(NEGO.position.x, NEGO.position.y + 0.7f, NEGO.position.z);
            shieldrotation = Quaternion.Euler(0, 315, 0);
        }
        if (SE)
        {
            shieldposition.Set(SEGO.position.x, SEGO.position.y + 0.7f, SEGO.position.z);
            shieldrotation = Quaternion.Euler(0, 225, 0);
        }
        if (SW)
        {
            shieldposition.Set(SWGO.position.x, SWGO.position.y + 0.7f, SWGO.position.z);
            shieldrotation = Quaternion.Euler(0, 135, 0);
        }
        if (N)
        {
            shieldposition.Set(NGO.position.x, NGO.position.y + 0.7f, NGO.position.z);
            shieldrotation = Quaternion.Euler(0, 90, 0);
        }
        if (E)
        {
            shieldposition.Set(EGO.position.x, EGO.position.y + 0.7f, EGO.position.z);
            shieldrotation = Quaternion.Euler(0, 0, 0);
        }
        if (S)
        {
            shieldposition.Set(SGO.position.x, SGO.position.y + 0.7f, SGO.position.z);
            shieldrotation = Quaternion.Euler(0, 90, 0);
        }
        if (W)
        {
            shieldposition.Set(WGO.position.x, WGO.position.y + 0.7f, WGO.position.z);
            shieldrotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown("l"))                             //if the block button has been pressed(l)
        {       
            clone = Instantiate(shieldInstance, shieldposition, shieldrotation); 
            clone.transform.parent = Player.transform;
        }
    }

    // Hunter was here 
    public void BoolGetter(bool Np, bool NWp, bool Wp, bool SWp, bool Sp, bool SEp, bool Ep, bool NEp)
    {
        N = Np;
        NW = NWp;
        W = Wp;
        SW = SWp;
        S = Sp;
        SE = SEp;
        E = Ep;
        NE = NEp;
    }
}
