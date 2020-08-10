using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        Transform cam = Camera.main.transform;
        transform.LookAt(cam);
        transform.rotation = cam.transform.rotation;
    }
}
