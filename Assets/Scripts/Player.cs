using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour

{
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

    [SerializeField] float xThrow = 0f;
    [SerializeField] float yThrow = 0f;

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
        float pitch = startingRotation.eulerAngles.x + transform.localPosition.y * YPositionPitchFactor + yThrow * YThrowPitchFactor;
        float yaw = startingRotation.eulerAngles.y + transform.localPosition.x * XPositionYawFactor + xThrow * XThrowYawFactor;
        float roll = startingRotation.eulerAngles.z + xThrow * XPositionRollFactor;
        transform.localRotation = Quaternion.Euler(roll, yaw, pitch);
    }
}
