using UnityEngine;
using CryptRush.Core;

namespace CryptRush.Obstacle
{
    public class Spiker : MonoBehaviour
    {
        [SerializeField] float minPierceValue = 0.1f;
        [SerializeField] float maxPierceValue = 0.5f;
        [Header("Visual Effect Settings")]
        [SerializeField] bool shakesCamera = false;
        [SerializeField] float shakeDuration = 0;
        [SerializeField] float shakeAmount = 0;
        [SerializeField] ParticleSystem dustParticles = null;
        [SerializeField] AudioSource audioSource = null;

        float pierceWaitTime;
        Collider col;
        Rigidbody rgb;
        CameraVFX camVFX;

        private void Awake()
        {
            rgb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            camVFX = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<CameraVFX>();

            pierceWaitTime = Random.Range(minPierceValue, maxPierceValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Obstacle")) { return; }

            Invoke("OnPierceGround", pierceWaitTime);    //Pierces the ground at different depths for a more organic look.
        }

        private void OnPierceGround()
        {
            StopSpike();
            PlayFX();
        }

        private void StopSpike()
        {
            Destroy(rgb);
            transform.tag = "Untagged";
            col.enabled = true;
            col.isTrigger = false;
        }

        private void PlayFX()
        {
            if (dustParticles != null) { dustParticles.Play(); }
            if (shakesCamera) { StartCoroutine(camVFX.CameraShake(shakeDuration, shakeAmount)); }
            if (audioSource != null) { audioSource.Play(); }
        }
    }
}