/*
 * Programmer:   Hunter Goodin 
 * Date Created: 03/12/2019 @  8:15 PM 
 * Last Updated: 03/12/2019 @  8:30 PM by Hunter Goodin
 * File Name:    CoreMovement.cs 
 * Description:  This script will be responsible for the camera's movement. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;   // To be populated with the player in-engine 
    private Vector3 offset;     // The offset the camera will be from the player 

    void Start()
    {
        // offset is = to wherever the camera is on initialization subtracted by where the player is on initialization 
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // move the camera to wherever the player is plus the offset 
        transform.position = player.transform.position + offset;
    }
}
