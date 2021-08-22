using UnityEngine;

namespace CryptRush.Obstacle
{
    public class RollingBoulder : MonoBehaviour
    {
        [SerializeField] float speed = 0;
        [SerializeField] float maxYSpeed = 5;
        [SerializeField] Vector3 moveDirection = Vector3.zero;

        Vector3 directionalSpeed;
        Rigidbody rgb;

        private void Awake() { rgb = GetComponent<Rigidbody>(); }

        private void Update() { RollBall(); }

        private void RollBall()
        {
            directionalSpeed = moveDirection.normalized * speed;
            directionalSpeed.y = Mathf.Clamp(rgb.velocity.y, -maxYSpeed, maxYSpeed);
            rgb.velocity = directionalSpeed;
        }
    }
}
