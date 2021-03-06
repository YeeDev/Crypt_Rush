using System.Collections;
using UnityEngine;
using CryptRush.ObstacleManagement;

namespace CryptRush.Obstacle
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] float timeToExpire = 5;

        ArrowPooler pooler;

        //Called in ArrowPooler
        public ArrowPooler SetArrowPooler { set => pooler = value; }

        private void OnEnable() { StartCoroutine(ExpireArrow()); }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Trap Activator") && !other.CompareTag("Shooter")) { ReEnqueueArrow(); }
        }

        private IEnumerator ExpireArrow()
        {
            if (timeToExpire <= Mathf.Epsilon) { yield break; }

            yield return new WaitForSeconds(timeToExpire);

            ReEnqueueArrow();
        }

        private void ReEnqueueArrow() { pooler.EnqueueArrow(gameObject); }
    }
}