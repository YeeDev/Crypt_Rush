using System.Collections;
using UnityEngine;
using CryptRush.UI;
using CryptRush.Core;

namespace CryptRush.Control
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] int timeBeforeLosing = 60;

        StateHandler state;
        LevelLoader loader;
        UIUpdater uI;

        //Got in CollisionHandler.
        public int GetLastingTime { get => timeBeforeLosing; }

        private void Awake()
        {
            loader = FindObjectOfType<LevelLoader>();
            uI = FindObjectOfType<UIUpdater>();
            state = FindObjectOfType<StateHandler>();

            StartCoroutine(RunTimer());
        }

        private IEnumerator RunTimer()
        {
            uI.UpdateTimer(timeBeforeLosing);

            for (int i = timeBeforeLosing; i >= 0; i--)
            {
                uI.UpdateTimer(i);

                yield return new WaitForSeconds(1);
                if (state.GetCurrentState != GameState.Playing) { yield break; }

                timeBeforeLosing--;
            }

            //TODO Change this to something else.
            loader.UILoadLevel(0);
        }
    }
}