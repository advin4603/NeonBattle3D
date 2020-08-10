using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootScript : MonoBehaviour
{
    GunScript parent;
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
    private bool isAuto
    {
        get { return parent.isAuto; }
    }
    private GameObject bulletPrefab
    {
        get { return parent.bulletPrefab; }
    }
    private float impactForce
    {
        get { return parent.impactForce; }
    }
    public Camera fpsCam;
    public ParticleSystem mf;
    void Start()
    {
        parent = gameObject.GetComponentInParent<GunScript>();

    }

    public void Shoot()
    {
        RaycastHit[] hits = Physics.RaycastAll(fpsCam.transform.position, fpsCam.transform.forward, range).OrderBy(h => h.distance).ToArray();

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (transform.InverseTransformPoint(hit.point).z > 0)
                {
                    PlayerInfo target = hit.transform.GetComponent<PlayerInfo>();
                    if (target != null)
                    {
                        if (hit.rigidbody != null)
                        {
                            BulletBehaviour newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletBehaviour>();
                            Vector3 bulletPath = hit.point - transform.position;
                            newBullet.maxDistance = bulletPath.magnitude;
                            newBullet.transform.forward = bulletPath.normalized;
                            hit.rigidbody.AddForce(-hit.normal * impactForce);
                            newBullet.target = target;
                            newBullet.damage = damage;
                        }
                        mf.Play();
                        return;

                    }
                }
            }
        }


        BulletBehaviour newBullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletBehaviour>();
        Vector3 bulletPoint = fpsCam.transform.position + range * fpsCam.transform.forward;
        Vector3 bulletPath2 = bulletPoint - transform.position;
        newBullet2.maxDistance = bulletPath2.magnitude;
        newBullet2.transform.forward = bulletPath2.normalized;
        mf.Play();
    }
}
