using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnPress : MonoBehaviour {

    public GameObject arrowPrefab;
    public Transform playerPos;
    public float arrowSpeed;
    public GameObject arrowSpawnXplus;
    public GameObject arrowSpawnZplus;
    public GameObject arrowSpawnXminus;
    public GameObject arrowSpawnZminus;


    void Update () {
        bool isNorth = false;
        bool isSouth = false;
        bool isEast = false;
        bool isWest = false;


        if (Input.GetKeyDown("a"))
        {
            isNorth = false;
            isSouth = false;
            isEast = false;
            isWest = true;

        }
        if (Input.GetKeyDown("s"))
        {
            isNorth = false;
            isSouth = true;
            isEast = false;
            isWest = false;

        }
        if (Input.GetKeyDown("w"))
        {
            isNorth = true;
            isSouth = false;
            isEast = false;
            isWest = false;

        }
        if (Input.GetKeyDown("d"))
        {
            isNorth = false;
            isSouth = false;
            isEast = true;
            isWest = false;

        }




        if (Input.GetKeyDown("f"))
        {
           
           
            
            if (isNorth)
            {
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnXplus.position, new Quaternion(playerPos.rotation.x, 90, playerPos.rotation.z, 0));
                instArrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(arrowSpeed, 10, arrowSpeed));
            }
            if (isEast)
            {
                var instArrow = (GameObject)Instantiate(arrowPrefab, playerPos.position, new Quaternion(playerPos.rotation.x, 90, playerPos.rotation.z, 0));
                instArrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(arrowSpeed, 10, arrowSpeed));
            }
            if (isSouth)
            {
                var instArrow = (GameObject)Instantiate(arrowPrefab, playerPos.position, new Quaternion(playerPos.rotation.x, 90, playerPos.rotation.z, 0));
                instArrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(arrowSpeed, 10, arrowSpeed));
            }
            if (isWest)
            {
                var instArrow = (GameObject)Instantiate(arrowPrefab, playerPos.position, new Quaternion(playerPos.rotation.x, 90, playerPos.rotation.z, 0));
                instArrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(arrowSpeed, 10, arrowSpeed));
            }
        }
    }
}
