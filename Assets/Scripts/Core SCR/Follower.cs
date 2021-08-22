using UnityEngine;

namespace CryptRush.Core
{
    public class Follower : MonoBehaviour
    {
        //Use an empty object and child the camera to it.
        //No Offset required.

        [SerializeField] float followSmooth = 1;

        Vector3 velocity = Vector3.zero;
        Transform player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //Avoids slow camera initial lock.
        private void Start() { transform.position = CalculatePosition(); }

        void FixedUpdate()
        {
            if (player == null) { return; }

            transform.position = Vector3.SmoothDamp(transform.position, CalculatePosition(), ref velocity, followSmooth);
        }

        private Vector3 CalculatePosition()
        {
            Vector3 followPosition = player.position;
            followPosition.y = transform.position.y;
            return followPosition;
        }
    }
}