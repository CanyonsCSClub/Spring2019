using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    private int damage = 50;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Health>().ChangeHealth(-damage);

            if (col.gameObject.name == "Orc")
            {
                col.gameObject.GetComponent<BigEnemy>().StartHit();
            }
            if (col.gameObject.name == "ShootyEnemy")
            {
                col.gameObject.GetComponent<ShootyEnemy>().StartHit();
            }
            if (col.gameObject.name == "MushRed" || col.gameObject.name == "MushGreen" || col.gameObject.name == "MushBlue")
            {
                col.gameObject.GetComponent<BasicEnemy>().StartHit();
            }
        }
    }

    public void Me6()
    {
        Debug.Log("Worked"); 
    }
}
