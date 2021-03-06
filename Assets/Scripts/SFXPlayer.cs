using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptRush.Sound
{
    public class SFXPlayer : MonoBehaviour
    {
        [SerializeField] bool playsSound = true;
        [SerializeField] AudioSource audioSource = null;
        [SerializeField] bool setRandomOffset = true;

        private void Awake()
        {
            if (setRandomOffset)
            {
                audioSource.time = Random.Range(0, audioSource.clip.length);
            }
        }

        public void PlaySound() { if (playsSound) { audioSource.Play(); } }

        public void PlayClip(AudioClip clip) { audioSource.PlayOneShot(clip); }
    }
}