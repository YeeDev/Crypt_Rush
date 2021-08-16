using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float rotationSpeed = 40;

    Vector3 moveDirection;
    Rigidbody rgb;

    void Awake()
    {
        rgb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SetMovingDirection();
        RotateCharacter();
    }

    private void FixedUpdate()
    {
        moveDirection.y = rgb.velocity.y;
        rgb.velocity = moveDirection.normalized * moveSpeed;
    }

    private void RotateCharacter()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (Equals(moveDirection, Vector3.zero)) { return; }

            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotationSpeed);
        }
    }

    private void SetMovingDirection()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");
    }
}
