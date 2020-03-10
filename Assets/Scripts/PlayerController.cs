using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class PlayerController : MonoBehaviour

{
    //Movement Factors
    [Header("Movement Speed")]
    [Tooltip("In m/s")] [SerializeField] float XSensitivity = 30f;
    [Tooltip("In m/s")] [SerializeField] float YSensitivity = 30f;

    [Header("Movement Bounds")]
    [SerializeField] float MaxXPos = 7f;
    [SerializeField] float YMax = 3.5f;
    [SerializeField] float YMin = 3.5f;

    [Header("Roll/Pitch/Yaw")]
    [SerializeField] float XPositionRollFactor = 20f;
    [SerializeField] float YPositionPitchFactor = -1.5f;
    [SerializeField] float YThrowPitchFactor = 25f;
    [SerializeField] float XThrowYawFactor = 25f;
    [SerializeField] float XPositionYawFactor = 1.5f;

    [Header("Weapons")]
    [SerializeField] GameObject[] guns;


    //Joystick positions from CrossPlatformInputManager
    float xThrow, yThrow;
    Quaternion startingRotation;

    bool isDead = false;

    void Start()
    {
        startingRotation = transform.localRotation;
    }

    void Update()
    {
        if (!isDead)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
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

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ToggleGuns(true);
        }
        else
        {
            ToggleGuns(false);
        }
    }

    private void ToggleGuns(bool isFiring)
    {
        foreach (GameObject gun in guns) {
            {
                gun.SetActive(isFiring);
            }
        }
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

    void ReceiveDeathMessage()
    {
        isDead = true;
        ToggleGuns(false);
    }
}
