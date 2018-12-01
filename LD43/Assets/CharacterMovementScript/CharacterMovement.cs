using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterMovement : NetworkBehaviour
{

    private float g = -15f;
    private CharacterController cc;

    private Vector3 velocity = Vector3.zero;
    private Player player;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
        player = gameObject.GetComponent<Player>();
    }


    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!cc.isGrounded)
        {
            ApplyGravity();
        }
        else
        {
            
            GetJump();
        }
        GetMovement();
        ApplyMovement();
    }


    void GetMovement()
    {
        float sprint = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift)) sprint = player.sprintMultiplier;
        Vector2 move;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        velocity.x = move.normalized.x * player.speed * sprint;
        velocity.z = move.normalized.y * player.speed * sprint;
    }


    void GetJump()
    {
        if (Input.GetButtonDown("Jump"))
            velocity.y = player.jumpHeight;
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
