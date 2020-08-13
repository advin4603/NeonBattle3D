using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TargeterScript : MonoBehaviour
{
    public GameObject target;

    private Camera cam;

    public float range = 500f;

    public GameObject reticle;
    public GameObject pointer;

    private SpriteRenderer reticleSprite;
    private SpriteRenderer pointerSprite;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        reticleSprite = reticle.GetComponent<SpriteRenderer>();
        reticleSprite.enabled = false;
        pointerSprite = pointer.GetComponent<SpriteRenderer>();
        pointerSprite.enabled = false;
    }

    void TargetSetter()
    {
        if (Input.GetButtonDown("Set Target"))
        {

            RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, range).OrderBy(h => h.distance).ToArray();

            if (hits.Length > 0)
            {
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
                                return;
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
        TargetUpdater();

    }
    void TargetUpdater()
    {
        if (target != null)
        {
            Vector3 projPoint = (cam.WorldToViewportPoint(target.transform.position)) - new Vector3(0.5f, 0.5f, 0f);
            if (-0.5f <= projPoint.x && 0.5f >= projPoint.x && 0.5f >= projPoint.y && -0.5f <= projPoint.y && 0 <= projPoint.z)
            {
                reticleSprite.enabled = true;
                pointerSprite.enabled = false;
                reticle.transform.localPosition = new Vector3(projPoint.x * Screen.width / 2, projPoint.y * Screen.height / 2, -8);
            }
            else
            {
                reticleSprite.enabled = false;
                pointerSprite.enabled = true;
                if (projPoint.z < 0)
                {
                    projPoint *= -1;
                }
                float inclination = Mathf.Atan2(projPoint.y, projPoint.x);
                pointer.transform.localRotation = Quaternion.Euler(0, 0, inclination * Mathf.Rad2Deg - 90);

                float cos = Mathf.Cos(inclination);
                float sin = Mathf.Sin(inclination);

                float ry = Screen.height / 5;
                float rx = Screen.width / 5;
                pointer.transform.localPosition = new Vector3(rx * cos, ry * sin, 0);
            }
        }else{
            reticleSprite.enabled = false;
            pointerSprite.enabled = false;
        }
    }
}
