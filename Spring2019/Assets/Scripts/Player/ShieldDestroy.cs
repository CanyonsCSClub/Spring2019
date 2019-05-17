using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDestroy : MonoBehaviour
{
    public GameObject itself;

    void Update ()
    {
        if (Input.GetKeyUp("l"))
        {
            Destroy(itself);
        }
    }
}
