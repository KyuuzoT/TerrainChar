using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private PlayerSounds playerSounds;
    private CharacterController controller { get; set; }

    [Header("Movement preferences")]
    [SerializeField] private float stepTimer = 0.0f;
    [SerializeField] private float stepRecharge = 1.25f;
    private bool isStepWait => stepTimer > 0;

    [SerializeField] private float walkingSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;
    [SerializeField] private float verticalSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    private float jumpAxis { get; set; }
    private float currentSpeed { get; set; }

    [Header("Shooting preferences")]
    [SerializeField] private float bulletSpeed = 30.0f;
    [SerializeField] private float shootingTimer = 5.0f;
    [SerializeField] private float rechargeTime = 5.0f;
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform bulletBirthPoint;
    private bool isRecharging => shootingTimer > 0;

    private bool isInAir { get; set; }

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
        playerSounds.Init(controller);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToLookDirection();

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

    private void FixedUpdate()
    {
        currentSpeed = walkingSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = sprintSpeed;
        }

        if(stepTimer > 0)
        {
            stepTimer -= Time.fixedDeltaTime;
            if(stepTimer <= 0)
            {
                stepTimer = 0;
            }
        }

        MoveCharacter(currentSpeed);

        if(controller.isGrounded && isInAir)
        {
            isInAir = false;
            playerSounds.PlayLandingSound();
        }
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
                playerSounds.PlayJumpingSound();
                isInAir = true;
            }
        }

        transferVector += transform.up * verticalSpeed * Time.fixedDeltaTime;
        controller.Move(transferVector);

        if(!isInAir && controller.velocity.sqrMagnitude > 0)
        {
            PlayStepsSound();
        }
    }

    private void PlayStepsSound()
    {
        if(!isStepWait && controller.isGrounded)
        {
            if (currentSpeed < sprintSpeed)
            {
                stepTimer = stepRecharge;
            }
            else if (currentSpeed >= sprintSpeed)
            {
                stepTimer = stepRecharge * (walkingSpeed/sprintSpeed);
            }

            playerSounds.PlayWalkingSound();
        }
    }

    private void RotateToLookDirection()
    {
        mouseLook.LookRotation(controller, cameraTransform);
    }
}
