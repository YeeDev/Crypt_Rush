using UnityEngine;
using CryptRush.Movement;
using CryptRush.Animation;
using CryptRush.Stats;
using CryptRush.Core;

namespace CryptRush.Control
{
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform groundChecker = null;
        [SerializeField] float checkerRadius = 0.44f;
        [SerializeField] LayerMask groundLayer = 0;

        bool grounded;
        Vector3 inputDirection;
        PlayerMover mover;
        PlayerAnimator anim;
        StateHandler state;

        private void Awake()
        {
            mover = GetComponent<PlayerMover>();
            anim = GetComponent<PlayerAnimator>();
            state = FindObjectOfType<StateHandler>();
        }

        private void Update()
        {
            if (state.GetCurrentState != GameState.Playing) { return; }

            ReadAxisInput();
            ReadJumpInput();
        }

        private void FixedUpdate()
        {
            if (state.GetCurrentState != GameState.Playing) { return; }

            grounded = Physics.CheckSphere(groundChecker.position, checkerRadius, groundLayer);
            anim.SetBoolAnimation("IsFalling", !grounded);

            mover.MovePlayer(inputDirection.normalized);
        }

        private void ReadAxisInput()
        {
            anim.RotateCharacter(Input.GetButton("Horizontal") || Input.GetButton("Vertical"), inputDirection.normalized);

            inputDirection.x = Input.GetAxis("Horizontal");
            inputDirection.z = Input.GetAxis("Vertical");
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown("Jump") && grounded) { mover.Jump(); }
            else if (Input.GetButtonUp("Jump")) { mover.HaltJump(); }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(groundChecker.position, checkerRadius);
        }
    }
}
