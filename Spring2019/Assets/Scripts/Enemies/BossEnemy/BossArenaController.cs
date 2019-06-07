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

    public GameObject possy1;
    public GameObject possy2;
    public GameObject possy3;
    public GameObject possy4;
    public GameObject possy5;
    public GameObject possy6;

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
            possy1.SetActive(true);
            possy2.SetActive(true);
            possy3.SetActive(true);
            possy4.SetActive(true);
            possy5.SetActive(true);
            possy6.SetActive(true);
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
