using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGunScript : MonoBehaviour
{
    public float damage;
    public float range;

    public float fireRate;
    private float nextTimeToFire = 0f;

    public bool isAuto;
    public GameObject bulletPrefab;
    public float impactForce;

    private bool left;

    public GameObject leftShooter;
    public GameObject rightShooter;

    private AIShooter leftShooterScript;
    private AIShooter rightShooterScript;
    void Start()
    {
        leftShooterScript = leftShooter.GetComponent<AIShooter>();
        rightShooterScript = rightShooter.GetComponent<AIShooter>();
    }
    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if (left)
            {
                leftShooterScript.Shoot();
            }
            else
            {
                rightShooterScript.Shoot();
            }
            left = !left;
        }
    }
}
