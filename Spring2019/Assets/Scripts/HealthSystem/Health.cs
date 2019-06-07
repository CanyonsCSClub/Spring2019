using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Health : MonoBehaviour
{
	public int health = 100;
    public int maxHealth = 100;
    public Text displayHealth;
    public GameObject healthBarObj;
    private Rigidbody rb;
    private bool isDead; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (health > maxHealth)
        { health = maxHealth; }
        if (health < 0)
        { health = 0; }

        healthBarObj.GetComponent<HealthBar>().HealthGetter(health);

        displayHealth.text = "" + health.ToString();

        if (health <= 0)
        {
            if (gameObject.name == "Orc")
            {
                gameObject.GetComponent<BigEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false; 
            }
            if (gameObject.name == "ShootyEnemy")
            {
                gameObject.GetComponent<ShootyEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "MushRed")
            {
                gameObject.GetComponent<BasicEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "MushGreen")
            {
                gameObject.GetComponent<BasicEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "MushBlue")
            {
                gameObject.GetComponent<BasicEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
            if (gameObject.name == "Orcutoryx The Scourge")
            {
                gameObject.GetComponent<BossEnemy>().StartDeath();
                Destroy(healthBarObj);
                gameObject.GetComponent<Health>().enabled = false;
            }
        }
    }

    public void ChangeHealth(int adj)
    {
        health = health + adj;
    }
}