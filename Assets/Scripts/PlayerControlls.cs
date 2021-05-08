using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private float yThrow;
    private float xThrow;
    
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] private float controlSpeed = 10f;
    [Tooltip("Range in x direction for clamping ship movement horizontally")]
    [SerializeField] private float xRange = 5f;
    [Tooltip("Range in y direction for clamping ship movement vertically")]
    [SerializeField] private float yRange = 5f;
    
    [Header("Laser gun Array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] private GameObject[] lasers;
    
    [Header("Screen position based Tweaking")]
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float positionYawFactor = 3f;
    
    [Header("Player Input Based tuning")]
    [SerializeField] private float controlPitchFactor = -10f;
    [SerializeField] private float controlRollFactor = -20f;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newXpos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXpos, -xRange, xRange);
        
        
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYpos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYpos, -yRange, yRange);

        transform.localPosition = new Vector3(
            clampedXPos, 
            clampedYPos, 
            transform.localPosition.z
        );
    }
    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }
    
    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
   

}
