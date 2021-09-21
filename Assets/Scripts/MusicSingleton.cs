using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptRush.Sound
{
    public class MusicSingleton : MonoBehaviour
    {
        MusicSingleton musicPlayer;

        public bool IsSingletonCreated { get => musicPlayer != null; }

        private void Awake()
        {
            foreach (MusicSingleton musicPlayer in FindObjectsOfType<MusicSingleton>())
            {
                if (musicPlayer.IsSingletonCreated)
                {
                    Destroy(gameObject);
                }
            }

            musicPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}