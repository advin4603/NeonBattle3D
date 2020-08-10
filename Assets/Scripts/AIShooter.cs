using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooter : MonoBehaviour
{
    AIGunScript parent;
    private float damage
    {
        get { return parent.damage; }
    }
    private float range
    {
        get { return parent.range; }
    }
    private float fireRate
    {
        get { return parent.fireRate; }
    }

    private GameObject bulletPrefab
    {
        get { return parent.bulletPrefab; }
    }
    private float impactForce
    {
        get { return parent.impactForce; }
    }

    public ParticleSystem mf;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<AIGunScript>();
    }

    // Update is called once per frame
    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            PlayerInfo target = hit.transform.GetComponent<PlayerInfo>();
            MainPlayerInfo playerTarget = hit.transform.GetComponent<MainPlayerInfo>();
            bool targetIsNull = target == null;
            bool playerTargetIsNull = playerTarget == null;
            if (!targetIsNull || !playerTargetIsNull)
            {
                if (hit.rigidbody != null)
                {
                    BulletBehaviour newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletBehaviour>();
                    Vector3 bulletPath = hit.point - transform.position;
                    newBullet.maxDistance = bulletPath.magnitude;
                    newBullet.transform.forward = bulletPath.normalized;
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                    newBullet.target = target;
                    newBullet.playerTarget = playerTarget;
                    newBullet.damage = damage;
                }
                mf.Play();
                return;
            }
        }
        BulletBehaviour newBullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletBehaviour>();
        newBullet2.maxDistance = range;
        newBullet2.transform.forward = transform.forward;
        mf.Play();
    }

}
