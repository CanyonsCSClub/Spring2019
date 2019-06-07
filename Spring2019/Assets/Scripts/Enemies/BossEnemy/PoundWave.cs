using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoundWave : MonoBehaviour
{
    private float incriment = 0.03f; 
	void Start ()
    {
        Destroy(gameObject, 0.9f);
	}

	void Update ()
    {
        gameObject.transform.localScale += new Vector3(incriment, incriment, incriment);

        if (GameObject.Find("Orcutoryx The Scourge").GetComponent<BossEnemy>().IsBossDead())
        {
            Destroy(gameObject);
        }
    }
}
