using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HitTaker))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float rotationSpeed = 40;

    Vector3 moveDirection;
    Rigidbody rgb;
    HitTaker hitTaker;

    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();
        hitTaker = GetComponent<HitTaker>();
    }

    private void Update()
    {
        if (!hitTaker.IsAlive()) { return; }

        SetMovingDirection();
        RotateCharacter();
    }

    private void FixedUpdate()
    {
        if (!hitTaker.IsAlive()) { return; }

        MovePlayer();
    }

    private void RotateCharacter()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Vector3 lookDirection = moveDirection;
            lookDirection.y = 0;

            //Prevents "0" warning.
            if (lookDirection.sqrMagnitude <= Mathf.Epsilon) { return; }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        }
    }

    private void SetMovingDirection()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = rgb.velocity.y;
        rgb.velocity = moveDirection;
    }
}
