using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float howLong = 2;
    private int damage = 25;
    private Vector3 direction; 

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            direction = (col.transform.position - GameObject.Find("Player").transform.position).normalized;
            // Debug.Log("direction = " + direction.ToString() + " on " + col.gameObject.name);
            col.gameObject.GetComponent<Health>().ChangeHealth(-damage);

            if(col.gameObject.name == "Orc")
            { col.gameObject.GetComponent<BigEnemy>().StartHit(); }
            if (col.gameObject.name == "ShootyEnemy")
            { col.gameObject.GetComponent<ShootyEnemy>().StartHit(); }
            if (col.gameObject.name == "MushRed")
            { col.gameObject.GetComponent<BasicEnemy>().StartHit(); }
            if (col.gameObject.name == "MushGreen")
            { col.gameObject.GetComponent<BasicEnemy>().StartHit(); }
            if (col.gameObject.name == "MushBlue")
            { col.gameObject.GetComponent<BasicEnemy>().StartHit(); }


            col.GetComponent<Rigidbody>().AddForce(direction * 500);
            Destroy(gameObject); 
        }
    }
}
