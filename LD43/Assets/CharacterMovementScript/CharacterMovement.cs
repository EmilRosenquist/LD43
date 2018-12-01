using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterMovement : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float airStrafeSpeed = 1.3f;
    [SerializeField]
    private float sprintMultiplier = 1.3f;
    [SerializeField]
    private float g = -9.82f;
    [SerializeField]
    private float jumpHeight = 10;
    private CharacterController cc;

    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }


    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!cc.isGrounded)
        {
            ApplyGravity();
            GetAirMovement();
        }
        else
        {
            GetGroundMovement();
            GetJump();
        }
        ApplyMovement();
    }


    void GetGroundMovement()
    {
        float sprint = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift)) sprint = sprintMultiplier;
        Vector2 move;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        velocity.x = move.normalized.x * speed * sprint;
        velocity.z = move.normalized.y * speed * sprint;
    }

    void GetAirMovement()
    {
        Vector2 VelocityXZ = new Vector2(velocity.x, velocity.z);
        Vector2 airStrafeVelocity = Vector2.zero;

        if (velocity.x >= 0 && Input.GetAxis("Horizontal") < 0 || velocity.x <= 0 && Input.GetAxis("Horizontal") > 0 || velocity.magnitude < speed)
        {
            airStrafeVelocity.x = Input.GetAxis("Horizontal");
        }
        if (velocity.z >= 0 && Input.GetAxis("Vertical") < 0 || velocity.z <= 0 && Input.GetAxis("Vertical") > 0 || velocity.magnitude < speed)
        {
            airStrafeVelocity.y = Input.GetAxis("Vertical");
        }
        airStrafeVelocity = airStrafeVelocity.normalized;

        velocity = new Vector3(velocity.x + airStrafeVelocity.x * airStrafeSpeed, velocity.y, velocity.z + airStrafeVelocity.y * airStrafeSpeed);
    }


    void GetJump()
    {
        if (Input.GetButtonDown("Jump"))
            velocity.y = jumpHeight;
    }


    void ApplyGravity()
    {
        velocity.y += g * Time.deltaTime;
    }


    void ApplyMovement()
    {
        velocity = transform.TransformDirection(velocity);
        cc.Move(velocity * Time.deltaTime);
    }
}
