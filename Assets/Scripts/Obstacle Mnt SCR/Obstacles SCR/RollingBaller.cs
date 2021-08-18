using UnityEngine;

namespace CryptRush.Obstacle
{
    public class RollingBaller : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] Vector3 moveDirection;

        Vector3 directionalSpeed;
        Rigidbody rgb;

        private void Awake()
        {
            rgb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            RollBall();
        }

        private void RollBall()
        {
            directionalSpeed = moveDirection.normalized * speed;
            directionalSpeed.y = rgb.velocity.y;
            rgb.velocity = directionalSpeed;
        }
    }
}
