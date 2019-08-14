using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnPress : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject bowPrefab; 
    public Transform playerPos;
    public float arrowSpeed;
    public GameObject arrowSpawnXplus;
    public GameObject arrowSpawnZplus;
    public GameObject arrowSpawnXminus;
    public GameObject arrowSpawnZminus;
    public GameObject Looooook; 

    bool isNorth = false;
    bool isSouth = false;
    bool isEast = false;
    bool isWest = false;

    public int fuckX;
    public int fuckY;
    public int fuckZ;
    public int fuckW; 

    private bool canShoot;
    public float arrowCooldown = 0.5f;

    public AudioSource ar; 

    private void Start()
    {
        isNorth = true;
        canShoot = true; 
    }

    IEnumerator Cooldown2()
    {
        yield return new WaitForSeconds(arrowCooldown);
        canShoot = true; 
    }

    void Update ()
    {
        if (Input.GetKeyDown("a"))
        {
            //Debug.Log("a");
            isNorth = false;
            isSouth = false;
            isEast = false;
            isWest = true;
        }
        if (Input.GetKeyDown("s"))
        {
            //Debug.Log("s");
            isNorth = false;
            isSouth = true;
            isEast = false;
            isWest = false;
        }
        if (Input.GetKeyDown("w"))
        {
            //Debug.Log("w");
            isNorth = true;
            isSouth = false;
            isEast = false;
            isWest = false;
        }
        if (Input.GetKeyDown("d"))
        {
            //Debug.Log("d");
            isNorth = false;
            isSouth = false;
            isEast = true;
            isWest = false;
        }


        // Made some changes to how the arrow was facing after I included some art for the arrows. 
        // Otherwise left the same. 
        // (Hunter) 
        if (Input.GetKeyDown("k") && canShoot)
        {
            if (isNorth)
            {
                //Debug.Log("is north shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnZplus.transform.position, new Quaternion(90, 90, 90, 90));
                ar.Play(); 
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, arrowSpeed));
                var tempBow = (GameObject)Instantiate(bowPrefab, new Vector3(arrowSpawnZplus.transform.position.x, arrowSpawnZplus.transform.position.y+1, arrowSpawnZplus.transform.position.z-0.5f), new Quaternion(90, 0, -90, 0), transform);
                Destroy(tempBow, 0.2f); 
            }
            if (isEast)
            {
                //Debug.Log("is east shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnXplus.transform.position, new Quaternion(90, 90, 0, 0));
                ar.Play();
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(arrowSpeed, 10, 0));
                var tempBow = (GameObject)Instantiate(bowPrefab, new Vector3(arrowSpawnXplus.transform.position.x - 0.5f, arrowSpawnXplus.transform.position.y + 1, arrowSpawnXplus.transform.position.z), new Quaternion(0, 90, 0, 0), transform);
                Destroy(tempBow, 0.2f);
            }
            if (isSouth)
            {
                //Debug.Log("is south shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnZminus.transform.position, new Quaternion(-90, -90, 90, 90));
                ar.Play();
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, -arrowSpeed));
                var tempBow = (GameObject)Instantiate(bowPrefab, new Vector3(arrowSpawnZminus.transform.position.x, arrowSpawnZminus.transform.position.y + 1, arrowSpawnZminus.transform.position.z + 0.5f), new Quaternion(90, 0, 90, 0), transform);
                Destroy(tempBow, 0.2f);
            }
            if (isWest)
            {
                //Debug.Log("is west shoot");
                var instArrow = (GameObject)Instantiate(arrowPrefab, arrowSpawnXminus.transform.position, new Quaternion(0, 0, -90, -90));
                ar.Play();
                instArrow.GetComponent<Rigidbody>().AddForce(new Vector3(-arrowSpeed, 10, 0));
                var tempBow = (GameObject)Instantiate(bowPrefab, new Vector3(arrowSpawnXminus.transform.position.x + 0.5f, arrowSpawnXminus.transform.position.y + 1, arrowSpawnXminus.transform.position.z), new Quaternion(0, 0, 0, 0), transform);
                Destroy(tempBow, 0.2f);
            }
            canShoot = false;
            StartCoroutine(Cooldown2()); 
        }
    }
}
