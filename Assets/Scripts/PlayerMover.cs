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

        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = rgb.velocity.y;
        rgb.velocity = moveDirection;
    }

    private void RotateCharacter()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            //According to Unity documentation, the == operator checks for approximate equality.
            Vector3 lookDirection = moveDirection;
            lookDirection.y = 0;

            if (lookDirection.sqrMagnitude <= Mathf.Epsilon) { return; }

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        }
    }

    private void SetMovingDirection()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");
    }
}
