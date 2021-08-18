using UnityEngine;

namespace CryptRush.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 40f;

        Animator anm;

        private void Awake()
        {
            anm = GetComponent<Animator>();
        }

        //Called in CollisionHandler.
        public void TriggerAnimation(string animation) { anm.SetTrigger(animation); }

        //Called in PlayerController.
        public void RotateCharacter(bool isMoving, Vector3 moveDirection)
        {
            if (!isMoving) { return; }

            Vector3 lookDirection = moveDirection;
            lookDirection.y = 0;

            if (lookDirection.sqrMagnitude <= Mathf.Epsilon) { return; }

            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        }
    }
}