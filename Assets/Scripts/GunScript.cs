using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
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

    private ShootScript leftShooterScript;
    private ShootScript rightShooterScript;
    void Start()
    {
        leftShooterScript = leftShooter.GetComponent<ShootScript>();
        rightShooterScript = rightShooter.GetComponent<ShootScript>();
    }

    void Update()
    {
        bool fired;
        if (isAuto)
        {
            fired = Input.GetButton("Fire1");
        }
        else
        {
            fired = Input.GetButtonDown("Fire1");
        }

        if (fired)
        {
            Shoot();
        }
    }
    void Shoot()
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
