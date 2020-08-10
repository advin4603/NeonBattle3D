using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControlloer : MonoBehaviour
{
    public GameObject player;
    public AIGunScript shooter;

    public float lookRadius;
    // Start is called before the first frame update
    void Start()
    {
        shooter = gameObject.GetComponentInChildren<AIGunScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        lookRadius = (player.transform.position - shooter.transform.position).magnitude - 30;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude <= lookRadius && Vector3.Dot((player.transform.position - transform.position).normalized, transform.forward) >= 0)
        {
            transform.LookAt(player.transform.position);
            shooter.Shoot();
        }
        else
        {
            transform.Translate(new Vector3(0, 0, 0.5f) * Time.deltaTime, Space.Self);
        }
    }
}
