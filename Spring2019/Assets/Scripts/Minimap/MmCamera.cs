using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MmCamera : MonoBehaviour {

    public Vector3 offset;
    public GameObject player;
    public Transform Minicamera;

    // Update is called once per frame
    void Update () {
        //change position of mini map camera to be above player
        Minicamera.position = player.transform.position + offset;
    }
}
