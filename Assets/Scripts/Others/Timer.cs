using System.Collections;
using UnityEngine;
using CryptRush.UI;
using CryptRush.Core;

namespace CryptRush.Control
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] int timeBeforeLosing = 60;

        LevelLoader loader;
        UIUpdater uI;

        private void Awake()
        {
            loader = FindObjectOfType<LevelLoader>();
            uI = FindObjectOfType<UIUpdater>();

            StartCoroutine(RunTimer());
        }

        private IEnumerator RunTimer()
        {
            for (int i = timeBeforeLosing; i >= 0; i--)
            {
                yield return new WaitForSeconds(1);

                uI.UpdateTimer(i.ToString());
            }

            //TODO Change this to something else.
            loader.LoadLevel(true);
        }
    }
}