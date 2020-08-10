using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float healthMax = 100;
    public float health;

    public HealthBarScript healthBar;

    private void Start()
    {
        health = healthMax;
        healthBar.SetHealth(1);
    }

    public void damage(float damagePoints)
    {
        health -= damagePoints;
        healthBar.SetHealth(health / healthMax);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
