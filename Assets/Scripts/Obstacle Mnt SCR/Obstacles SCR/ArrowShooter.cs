using System.Collections;
using UnityEngine;
using CryptRush.ObstacleManagement;

namespace CryptRush.Obstacle
{
    public class ArrowShooter : MonoBehaviour
    {
        [SerializeField] float fireRate = 2;
        [SerializeField] float shootDelay = 0;
        [SerializeField] float arrowSpeed = 2;

        Quaternion targetRotation;
        ArrowPooler pooler;

        private void Awake()
        {
            pooler = FindObjectOfType<ArrowPooler>();
            targetRotation = transform.rotation * pooler.GetArrowRotation;
        }

        private void Start()
        {
            StartCoroutine(ShootRepeteadly());
        }

        private IEnumerator ShootRepeteadly()
        {
            yield return new WaitForSeconds(shootDelay);

            while (true)
            {
                GameObject arrow = pooler.GetArrow();

                if (arrow == null)
                {
                    Debug.LogWarning("No Arrows Available");
                    yield return new WaitForSeconds(fireRate);
                    continue; //The "continue" function stops the loop at the point it is without stopping the loop.
                }

                InitializeArrow(arrow);

                yield return new WaitForSeconds(fireRate);
            }
        }

        private void InitializeArrow(GameObject arrow)
        {
            arrow.transform.position = transform.position;
            arrow.transform.rotation = targetRotation;
            arrow.SetActive(true);

            arrow.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;
        }
    }
}