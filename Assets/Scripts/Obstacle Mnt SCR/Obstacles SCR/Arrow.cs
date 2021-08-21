using System.Collections;
using UnityEngine;
using CryptRush.ObstacleManagement;

namespace CryptRush.Obstacle
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] float timeToExpire = 5;

        ArrowPooler pooler;

        public ArrowPooler SetArrowPooler { set => pooler = value; }

        private void OnEnable() { StartCoroutine(ExpireArrow()); }

        private void OnTriggerEnter(Collider other) { if (!other.CompareTag("Trap Activator")) { ReEnqueueArrow(); } }

        private IEnumerator ExpireArrow()
        {
            if (timeToExpire <= Mathf.Epsilon) { yield break; }

            yield return new WaitForSeconds(timeToExpire);

            ReEnqueueArrow();
        }

        private void ReEnqueueArrow() { pooler.EnqueueArrow(gameObject); }
    }
}