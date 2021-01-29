using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MouseLook
{
    [Header("Mouse Look")]
    private Quaternion characterRotation;
    private Quaternion cameraRotation;
    [SerializeField] private float sensivity = 3.0f;
    [SerializeField] private float minRadians = -0.79f;
    [SerializeField] private float maxRadians = 1.05f;

    internal void Init(CharacterController charController, Transform camera) => InternalInit(charController, camera);
    internal void LookRotation(CharacterController charController, Transform camera) => InternalLookRotation(charController, camera);

    private void InternalInit(CharacterController charController, Transform camera)
    {
        characterRotation = charController.transform.localRotation;
        cameraRotation = camera.localRotation;
    }

    private void InternalLookRotation(CharacterController charController, Transform camera)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        charController.transform.Rotate(Vector3.up, mouseX * sensivity);
        camera.Rotate(Vector3.right, -ClampAngle(mouseY)*sensivity, Space.Self);
    }

    private float ClampAngle(float mouseY)
    {
        return Mathf.Clamp(mouseY, minRadians, maxRadians);
    }
}
