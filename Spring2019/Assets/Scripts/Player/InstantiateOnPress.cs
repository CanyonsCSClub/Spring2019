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

    bool isNorth = false;
    bool isSouth = false;
    bool isEast = false;
    bool isWest = false;

    void Update () {



        if (Input.GetKeyDown("a"))
        {
            Debug.Log("a");
            isNorth = false;
            isSouth = false;
            isEast = false;
            isWest = true;

        }
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("s");
            isNorth = false;
            isSouth = true;
            isEast = false;
            isWest = false;

        }
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("w");
            isNorth = true;
            isSouth = false;
            isEast = false;
            isWest = false;

        }
        if (Input.GetKeyDown("d"))
        {
            Debug.Log("d");
            isNorth = false;
            isSouth = false;
            isEast = true;
            isWest = false;

        }




        if (Input.GetKeyDown("f"))
        {



            if (isNorth)
            {
                Debug.Log("is north shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnZplus.transform.position, new Quaternion(0, 90, 90, 0));
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, arrowSpeed));
            }
            if (isEast)
            {
                Debug.Log("is east shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnXplus.transform.position, new Quaternion(90, 90, 0, 0));
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(arrowSpeed, 10, 0));
            }
            if (isSouth)
            {
                Debug.Log("is south shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnZminus.transform.position, new Quaternion(0, 90, 90, 0));
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, -arrowSpeed));
            }
            if (isWest)
            {
                Debug.Log("is west shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnXminus.transform.position, new Quaternion(90, 90, 0, 0));
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(-arrowSpeed, 10, 0));
            }
        }
    }
}
