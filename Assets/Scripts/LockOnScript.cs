using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LockOnScript : MonoBehaviour
{

    public GameObject target;
    private SpriteRenderer mySprite;

    private Camera cam;

    private Vector3 oldA;

    public float range = 500f;

    public Sprite arrow;

    public Sprite targetReticle;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        mySprite.enabled = true;
    }

    void TargetSetter()
    {
        if (Input.GetButtonDown("Set Target"))
        {

            RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, range).OrderBy(h => h.distance).ToArray();

            if (hits.Length > 0)
            {
                Debug.Log(hits);
                foreach (RaycastHit hit in hits)
                {
                    if (transform.InverseTransformPoint(hit.point).z > 0)
                    {

                        PlayerInfo hitTarget = hit.transform.GetComponent<PlayerInfo>();
                        if (hitTarget != null)
                        {
                            if (hit.rigidbody != null)
                            {
                                target = hitTarget.gameObject;
                                Debug.Log(target);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        TargetSetter();
        if (target != null)
        {
            TargetSpriteUpdater();
        }
        else
        {
            mySprite.enabled = false;
        }
    }

    void TargetSpriteUpdater()
    {
        Vector3 a = (cam.WorldToViewportPoint(target.transform.position)) - new Vector3(0.5f, 0.5f, 0f);
        if (a != oldA)
        {
            if (a.z < 0)
            {
                mySprite.enabled = false;
            }
            else
            {
                mySprite.enabled = true;
                transform.localPosition = new Vector3(a.x * Screen.width / 2, a.y * Screen.height / 2, -8);
            }
            oldA = a;
        }
    }
}
