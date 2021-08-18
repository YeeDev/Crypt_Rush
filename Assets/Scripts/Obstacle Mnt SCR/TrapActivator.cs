using System.Collections;
using UnityEngine;

namespace CryptRush.ObstacleManagement
{
    public class TrapActivator : MonoBehaviour
    {
        [SerializeField] GameObject trapToActivate = null;
        [SerializeField] float activationDelay = 1;

        Collider triggerCollider;

        private void Awake()
        {
            triggerCollider = GetComponent<Collider>();
        }

        public IEnumerator ActivateTrap()
        {
            triggerCollider.enabled = false;

            yield return new WaitForSeconds(activationDelay);

            trapToActivate.SetActive(true); ;
        }
    }
}