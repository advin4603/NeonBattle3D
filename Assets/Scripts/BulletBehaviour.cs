using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public ParticleSystem hitSparkPrefab;
    public float speed;
    public float maxDistance;
    public Vector3 startPoint;

    public PlayerInfo target;

    public MainPlayerInfo playerTarget;

    public float damage = 10f;

    public float explosionRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    float distanceGetter()
    {
        return (transform.position + speed * Time.deltaTime * transform.forward - startPoint).magnitude;
    }

    void explode()
    {
        ParticleSystem explosion = Instantiate(hitSparkPrefab, transform.position, Quaternion.identity);
        explosion.Play();
        if (target != null && (target.transform.position - transform.position).magnitude <= explosionRadius)
        {
            target.damage(damage);
        }
        else if (playerTarget != null && (playerTarget.transform.position - transform.position).magnitude <= explosionRadius)
        {
            playerTarget.damage(damage);
        }
        Destroy(gameObject);
        Destroy(explosion.gameObject, 3f);
    }

    void FixedUpdate()
    {
        if (distanceGetter() >= maxDistance)
        {
            explode();
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
        }
    }
}
