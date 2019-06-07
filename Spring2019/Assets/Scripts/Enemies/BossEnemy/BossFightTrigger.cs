using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public GameObject bossArenaController; 

    private void OnTriggerExit(Collider col)
    {
        bossArenaController.GetComponent<BossArenaController>().ToggleBossFight();
    }
}
