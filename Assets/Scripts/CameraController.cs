using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Quaternion defaultRotation;
    public float XSensitivity;
    public float YSensitivity;
    void Start()
    {
        defaultRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Look"))
        {
            float mouseX = XSensitivity * Input.GetAxis("Mouse X");
            float mouseY = YSensitivity * -Input.GetAxis("Mouse Y");

            transform.Rotate(mouseY * Time.deltaTime, mouseX * Time.deltaTime, 0, Space.Self);
        }
        else if (Input.GetButtonUp("Look"))
        {
            transform.localRotation = defaultRotation;
        }
    }
}
