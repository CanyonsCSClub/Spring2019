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

    void Update()
    {
        if (health > maxHealth)
        { health = maxHealth; }
        if (health < 0)
        { health = 0; }

        healthBarObj.GetComponent<HealthBar>().HealthGetter(health);

        displayHealth.text = "" + health.ToString();
    }

    public void ChangeHealth(int adj)
    {
        health = health + adj;
    }
}