using UnityEngine;

namespace CryptRush.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HitTaker))]
    public class PlayerMover : MonoBehaviour
    {
        [Range(-10, 10)] [SerializeField] float moveSpeed = 3;
        [Range(-100, 100)] [SerializeField] float jumpForce = 50;
        [Range(-100, 100)] [SerializeField] float rotationSpeed = 40;
        [Range(-100, 100)] [SerializeField] float pushForce = 50;

        Rigidbody rgb;

        private void Awake()
        {
            rgb = GetComponent<Rigidbody>();
        }

        //Called in PlayerControl
        public void MovePlayer(Vector3 direction)
        {
            rgb.MovePosition(transform.position + direction * moveSpeed);
        }

        //Called in PlayerController
        public void Jump() { rgb.AddForce(Vector3.up * jumpForce); }

        //Called in PlayerController
        public void HaltJump()
        {
            if (rgb.velocity.y > 0)
            {
                Vector3 haltSpeed = rgb.velocity;
                haltSpeed.y *= 0.5f;
                rgb.velocity = haltSpeed;
            }
        }

        //Called in HitTaker
        public void PushRigidbody(Vector3 direction)
        {
            direction.y = transform.position.y;
            rgb.AddForce((transform.position - direction).normalized * pushForce, ForceMode.VelocityChange);
        }
    }
}