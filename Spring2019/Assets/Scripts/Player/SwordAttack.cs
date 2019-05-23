using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordAttack : MonoBehaviour

{
    public GameObject player;
    public Transform Spawnpoint;
    public GameObject Sword;
    void Update()
    {
        SwordSlash();
    }
    void SwordSlash()
    {
        if (Input.GetKeyDown("p"))
        {
            Instantiate(Sword, Spawnpoint.position, transform.rotation * Quaternion.Euler(90, 0, 0));
        }


    }
} 