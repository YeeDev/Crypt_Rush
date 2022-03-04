using UnityEngine;
using CryptRush.Core;

namespace CryptRush.Obstacle
{
    public class RollingBoulder : MonoBehaviour
    {
        [SerializeField] float speed = 0;
        [SerializeField] float maxYSpeed = 5;
        [SerializeField] Vector3 moveDirection = Vector3.zero;

        bool stopped;
        Vector3 directionalSpeed;
        Rigidbody rgb;
        CameraVFX cameraVFX;

        private void Awake()
        {
            rgb = GetComponent<Rigidbody>();
            cameraVFX = FindObjectOfType<CameraVFX>();
        }

        private void Update() { RollBall(); }

        private void RollBall()
        {
            directionalSpeed = moveDirection.normalized * speed;
            directionalSpeed.y = Mathf.Clamp(rgb.velocity.y, -maxYSpeed, maxYSpeed);
            rgb.velocity = directionalSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!stopped && other.CompareTag("Boulder Stopper"))
            {
                stopped = true;
                GetComponent<AudioSource>().Stop();
                StartCoroutine(cameraVFX.CameraShake(1, 0.1f));
            }
        }
    }
}
