using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float baseSpeed = 15;
    public float gravity = -9.81f;
    public float jumpHeight = 3;

    public Animator m_Animator;
    public float speedModifier = 3;

    Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public GameObject[] weapons;
    int activeWeapon = 0;

    InputHandler inputHandler;

    Vector3 velocity;
    bool isGrounded;
    bool isShooting;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        inputHandler = new InputHandler();

        groundCheck = GameObject.Find("GroundCheck").transform;
    }

    void Update()
    {
        CheckForGround();
        CheckForShooting();

        List<Command> commands = inputHandler.GetCommands();
        foreach (Command command in commands)
            command.execute(this);

        UpdateGravity();
    }

    void CheckForGround() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded) {
            m_Animator.SetBool("isJumping", false);
        }
        else {
            m_Animator.SetBool("isJumping", true);
        }
    }

    void CheckForShooting(){
        isShooting = hasGunEquiped() && inputHandler.checkForBasicAttackButtonDown();
    }

    void UpdateGravity() {
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump() {
        if (!isGrounded) return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void Run(Vector3 direction) {
        m_Animator.SetBool("isSprinting", true);

        Move(direction, baseSpeed * speedModifier);
    }

    public void Move(Vector3 direction, float speed) {
        if (speed < 0) {
            speed = baseSpeed;

            m_Animator.SetBool("isSprinting", false);
        }

        m_Animator.SetBool("isRunning", true);
        float targetAngle = GetTargetAngleTowardsCameraDirection(direction);

        if(!isShooting){
            RotatePlayer(targetAngle, turnSmoothTime);
        }

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        MovePlayer(moveDir.normalized * speed * Time.deltaTime);
    }

    public void MovePlayer(Vector3 direction) {
        controller.Move(direction);
    }

    public void RotatePlayer(float targetAngle, float smoothTime) {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public float GetTargetAngleTowardsCameraDirection(Vector3 direction) {
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
    }

    public void Idle() {
        m_Animator.SetBool("isRunning", false);
        m_Animator.SetBool("isSprinting", false);
    }

    public WeaponController GetActiveWeapon() {
        if (weapons.Length == 0) return null;

        return weapons[activeWeapon].GetComponent<WeaponController>();
    }

    public void SwapWeapon() {
        if (weapons.Length == 0) return;

        if (weapons[activeWeapon].GetComponent<WeaponController>().IsLocked()) return;

        weapons[activeWeapon].GetComponent<WeaponController>().Disable();

        activeWeapon = (activeWeapon + 1) % weapons.Length;

        weapons[activeWeapon].GetComponent<WeaponController>().Enable();
    }

    public bool hasGunEquiped(){
        return activeWeapon == 1;
    }
}
