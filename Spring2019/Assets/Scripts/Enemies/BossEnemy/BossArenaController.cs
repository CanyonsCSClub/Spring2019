using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaController : MonoBehaviour
{
    public GameObject bossFightTrigger;
    public GameObject boss;
    public GameObject arenaWall;
    public GameObject bossHealthText;
    public GameObject bossHealthBar; 

    public bool fightBegin; 
    
	void Update ()
    {
		if (fightBegin == true)
        {
            boss.GetComponent<BossEnemy>().ActiveToggle();
            ActivateArenaWalls();
            Destroy(bossFightTrigger);
            bossHealthText.SetActive(true);
            bossHealthBar.SetActive(true); 
        }
        fightBegin = false; 
	}

    public void ToggleBossFight()
    {
        fightBegin = true; 
    }

    public void ActivateArenaWalls()
    {
        arenaWall.SetActive(true); 
    }
}
