using System.Collections;
using UnityEngine;

namespace CryptRush.ObstacleManagement
{
    public class BallTeleporter : MonoBehaviour
    {
        [SerializeField] bool teleports = true;
        [SerializeField] float timeToRespawn = 5;
        [SerializeField] Transform boulder = null;

        Vector3 startingPosition;

        private void Awake()
        {
            startingPosition = boulder.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject colliderObject = other.gameObject;

            if (colliderObject.CompareTag("Obstacle"))
            {
                colliderObject.SetActive(false);

                if (teleports)
                {
                    StartCoroutine(RespawnBall(colliderObject));
                }
            }
        }

        private IEnumerator RespawnBall(GameObject ball)
        {
            yield return new WaitForSeconds(timeToRespawn);

            ball.transform.position = startingPosition;
            ball.SetActive(true);
        }
    }
}