using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour

{
    //Movement Factors
    [Tooltip("In m/s")] [SerializeField] float XSensitivity = 2f;
    [Tooltip("In m/s")] [SerializeField] float YSensitivity = 2f;
    [SerializeField] float MaxXPos = 10f;
    [SerializeField] float YMax = 5f;
    [SerializeField] float YMin = 5f;
    [SerializeField] float XPositionYawFactor = 3f;
    [SerializeField] float YPositionPitchFactor = -2f;
    [SerializeField] float XThrowYawFactor = 100f;
    [SerializeField] float YThrowPitchFactor = 100f;
    [SerializeField] float XPositionRollFactor = 20f;

    //Joystick positions from CrossPlatformInputManager
    float xThrow, yThrow;
    Quaternion startingRotation;

    void Start()
    {
        startingRotation = transform.localRotation;
    }


    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collided");
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Triggered");
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float dt = Time.deltaTime;
        float xoffset = xThrow * XSensitivity * dt;
        float yoffset = yThrow * YSensitivity * dt;

        float xpos = Mathf.Clamp(transform.localPosition.x + xoffset, -MaxXPos, MaxXPos);
        float ypos = Mathf.Clamp(transform.localPosition.y + yoffset, -YMax, YMin);
        float zpos = transform.localPosition.z;

        transform.localPosition = new Vector3(xpos, ypos, zpos);
    }

    void ProcessRotation()
    {
        float roll = CalculateRoll();
        float yaw = CalculateYaw();
        float pitch = CalculatePitch();
        transform.localRotation = Quaternion.Euler(roll, yaw, pitch);
    }

    private float CalculateRoll()
    {
        //In the world, the player ship is facing the -x axis, therefore X in the Euler vector controls Roll
        float startingRoll = startingRotation.eulerAngles.z;
        float rollDueToXThrow = xThrow * XPositionRollFactor;
        float roll = startingRoll + rollDueToXThrow;
        return roll;
    }

    private float CalculateYaw()
    {
        float initialYaw = startingRotation.eulerAngles.y;
        float yawDueToXPosition = transform.localPosition.x * XPositionYawFactor;
        float yawDueToXThrow = xThrow * XThrowYawFactor;
        float yaw = initialYaw + yawDueToXPosition + yawDueToXThrow;
        return yaw;
    }

    //In the world, the player ship is facing the -x axis, therefore Z in the Euler vector controls Pitch

    private float CalculatePitch()
    {
        float initialPitch = startingRotation.eulerAngles.x;
        float pitchDueToYPosition = transform.localPosition.y * YPositionPitchFactor;
        float pitchDueToYThrow = yThrow * YThrowPitchFactor;
        float pitch = initialPitch + pitchDueToYPosition + pitchDueToYThrow;
        return pitch;
    }
}
