using UnityEngine;

namespace CryptRush.Obstacle
{
    public class Spiker : MonoBehaviour
    {
        [SerializeField] float minPierceValue = 0.1f;
        [SerializeField] float maxPierceValue = 0.5f;

        float pierceWaitTime;
        Collider col;
        Rigidbody rgb;

        private void Awake()
        {
            rgb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();

            pierceWaitTime = Random.Range(minPierceValue, maxPierceValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) { return; }

            Invoke("PierceGround", pierceWaitTime);    //Pierces the ground at different depths for a more organic look.
        }

        private void PierceGround()
        {
            Destroy(rgb);
            transform.tag = "Untagged";
            col.enabled = true;
            col.isTrigger = false;
        }
    }
}