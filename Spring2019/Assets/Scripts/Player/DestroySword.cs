using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySword : MonoBehaviour
{
    public GameObject Sword;
    // Use this for initialization
    void Start()
    {
        Destroy(Sword, 1f);
    }
}
