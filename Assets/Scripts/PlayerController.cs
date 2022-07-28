using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Declare variables
    private Rigidbody playerRb;
    
    [SerializeField] float speed;
    [SerializeField] private float horsePower = 0;
    private const float turnSpeed = 25.0f;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float rpm;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    void Awake()
    {
        //must give each wheel a little torque or the wheel colliders will be stuck/not work properly
        foreach (WheelCollider w in allWheels)
            w.motorTorque = 0.000001f;
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // This is where we get player inout
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            // We move the vehicle forward
            // transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);


            // We turn the vehicle
            //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f); // For kph, change 2.237 to 3.6
            speedometerText.SetText("Speed: " + speed + "mph");

            rpm = Mathf.Round(speed % 30) * 40;
            rpmText.SetText("RPM: " + rpm);
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (!wheel.isGrounded)
            {
               return false;
            }
            
        }
        return true;
    }
}
