using UnityEngine;

namespace CryptRush.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        [Range(-10, 10)] [SerializeField] float moveSpeed = 3;
        [Range(-1000, 1000)] [SerializeField] float jumpForce = 50;
        [Range(-100, 100)] [SerializeField] float pushForce = 50;

        Rigidbody rgb;

        private void Awake() { rgb = GetComponent<Rigidbody>(); }

        //Called in PlayerController.
        public void MovePlayer(Vector3 direction)
        {
            rgb.MovePosition(transform.position + direction * moveSpeed);
        }

        //Called in PlayerController.
        public void Jump() { rgb.AddForce(Vector3.up * jumpForce); }

        //Called in PlayerController.
        public void HaltJump()
        {
            if (rgb.velocity.y > 0)
            {
                Vector3 haltSpeed = rgb.velocity;
                haltSpeed.y *= 0.5f;
                rgb.velocity = haltSpeed;
            }
        }

        //Called in CollisionHandler.
        public void Push(Vector3 pusherPosition)
        {
            pusherPosition.y = transform.position.y;
            rgb.AddForce((transform.position - pusherPosition).normalized * pushForce, ForceMode.VelocityChange);
        }

        //Called in CollisionHandler.
        public void Respawn(Vector3 checkpoint)
        {
            transform.position = checkpoint;
            rgb.velocity = Vector3.zero;
        }
    }
}