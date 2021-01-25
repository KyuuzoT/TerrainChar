﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private CharacterController controller;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float sensivity = 3.0f;
    [SerializeField] private float verticalSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;

    [SerializeField] private float bulletSpeed = 30.0f;
    [SerializeField] private float shootingTimer = 5.0f;
    [SerializeField] private float rechargeTime = 5.0f;
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform bulletBirthPoint;
    private bool isRecharging => shootingTimer > 0;
    

    // Start is called before the first frame update
    void Awake()
    {
        controller = transform.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToLookDiraction();

        if(shootingTimer > 0)
        {
            shootingTimer -= Time.deltaTime;
            if(shootingTimer <= 0)
            {
                shootingTimer = 0;
            }
        }
        Shoot();
        //MoveCharacter();
    }

    private void Shoot()
    {
        float fireAxis = Input.GetAxis("Fire1");
        if(fireAxis > 0 && !isRecharging)
        {
            var bulletRB = Instantiate(bullet).GetComponent<Rigidbody>();
            bulletRB.transform.position = bulletBirthPoint.position;
            bulletRB.transform.rotation = bulletBirthPoint.rotation;

            bulletRB.AddForce(bulletRB.transform.forward * bulletSpeed, ForceMode.Impulse);
            shootingTimer = rechargeTime;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 transferVector = (transform.forward * vertical * speed + transform.right * horizontal * speed) * Time.deltaTime;
        verticalSpeed = controller.velocity.y;
        verticalSpeed += Physics.gravity.y * Time.deltaTime;

        float jumpAxis = Input.GetAxis("Jump");
        if (jumpAxis > 0 && controller.isGrounded)
        {
            verticalSpeed = jumpForce;
        }

        transferVector += transform.up * verticalSpeed * Time.deltaTime;

        controller.Move(transferVector);
    }

    private void RotateToLookDiraction()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        controller.transform.Rotate(Vector3.up, mouseX * sensivity);
        cameraTransform.Rotate(Vector3.right, -mouseY * sensivity, Space.Self);
    }
}