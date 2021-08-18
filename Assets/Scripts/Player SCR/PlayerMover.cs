using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HitTaker))]
public class PlayerMover : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float rotationSpeed = 40;
    [SerializeField] float pushForce = 50;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 50;
    [SerializeField] Transform groundChecker = null;
    [SerializeField] float checkerRadius = 0.2f;
    [SerializeField] LayerMask groundLayer = 0;

    bool grounded;
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
        Jump();
    }

    private void FixedUpdate()
    {
        if (!hitTaker.IsAlive()) { return; }

        grounded = Physics.CheckSphere(groundChecker.position, checkerRadius, groundLayer);

        MovePlayer();
    }

    //Called in HitTaker
    public void PushRigidbody(Vector3 direction)
    {
        direction.y = transform.position.y;
        rgb.AddForce((transform.position - direction).normalized * pushForce, ForceMode.VelocityChange);
    }

    private void SetMovingDirection()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");
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

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rgb.AddForce(Vector3.up * jumpForce);
        }
        else if (Input.GetButtonUp("Jump") && rgb.velocity.y > 0)
        {
            Vector3 haltSpeed = rgb.velocity;
            haltSpeed.y *= 0.5f;
            rgb.velocity = haltSpeed;
        }
    }

    private void MovePlayer()
    {
        moveDirection = moveDirection.normalized * moveSpeed;
        rgb.MovePosition(transform.position + moveDirection);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundChecker.position, checkerRadius);
    }
}
