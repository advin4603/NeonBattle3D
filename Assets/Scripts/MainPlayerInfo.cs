using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerInfo : MonoBehaviour
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
    }

    private void OnCollisionEnter(Collision other)
    {
        damage(1);
        other.gameObject.GetComponent<PlayerInfo>().damage(1);
    }
}
