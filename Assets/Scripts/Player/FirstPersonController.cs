using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private MouseLook mouseLook;
    private CharacterController controller;

    [SerializeField] private float walkingSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;
    [SerializeField] private float verticalSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    private float jumpAxis { get; set; }
    private float currentSpeed { get; set; }

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

    private void Start()
    {
        mouseLook.Init(controller, cameraTransform);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToLookDiraction();

        jumpAxis = Input.GetAxis("Jump");

        if(shootingTimer > 0)
        {
            shootingTimer -= Time.deltaTime;
            if(shootingTimer <= 0)
            {
                shootingTimer = 0;
            }
        }
        Shoot();
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
        currentSpeed = walkingSpeed;
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = sprintSpeed;
        }
        MoveCharacter(currentSpeed);
    }

    private void MoveCharacter(float movementSpeed)
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 transferVector = (transform.forward * vertical * movementSpeed + transform.right * horizontal * movementSpeed) * Time.fixedDeltaTime;
        verticalSpeed = controller.velocity.y;
        verticalSpeed += Physics.gravity.y * Time.fixedDeltaTime;

        if(controller.isGrounded)
        {
            if (jumpAxis > 0)
            {
                verticalSpeed = jumpForce;
            }
        }

        transferVector += transform.up * verticalSpeed * Time.fixedDeltaTime;

        controller.Move(transferVector);
    }

    private void RotateToLookDiraction()
    {
        mouseLook.LookRotation(controller, cameraTransform);
    }
}
