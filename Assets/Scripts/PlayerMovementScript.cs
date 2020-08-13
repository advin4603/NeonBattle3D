using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float horizontalTorque;
    public float verticalTorque;
    public float rollTorque;

    private float horizontalInput = 0f;

    private float verticalInput = 0f;

    private float rollInput = 0f;
    public float maxSpeed;

    public float maxAngularSpeed;
    public float thrustPower;

    private float thrustDir;

    public Rigidbody rb;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Mouse X");
        verticalInput = -Input.GetAxisRaw("Mouse Y");
        rollInput = Input.GetAxis("Roll");
        if (Input.GetButton("Look"))
        {
            horizontalInput = 0;
            verticalInput = 0;
            rollInput = 0;
        }

        thrustDir = Input.GetAxis("Thrust");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Thrust(thrustPower * thrustDir);
        Rotate(new Vector3(verticalTorque * verticalInput, horizontalTorque * horizontalInput, rollTorque * rollInput));
    }

    void Thrust(float thrustAmt)
    {
        if (rb.velocity.magnitude <= maxSpeed)
        {
            rb.AddRelativeForce(new Vector3(0f, 0f, thrustAmt));
        }
    }

    void Rotate(Vector3 torqueAmt)
    {
        if (rb.angularVelocity.magnitude <= maxAngularSpeed)
        {
            rb.AddRelativeTorque(torqueAmt);
        }
    }
}
