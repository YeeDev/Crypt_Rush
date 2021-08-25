using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryptRush.Core;

namespace CryptRush.Control
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu = null;

        bool paused;
        StateHandler state;

        private void Awake() { state = FindObjectOfType<StateHandler>(); }

        private void Update() { ReadPauseInput(); }

        private void ReadPauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
                Time.timeScale = paused ? 0 : 1;
                state.SetState = paused ? GameState.NotPlaying : GameState.Playing;
                pauseMenu.SetActive(paused);
            }
        }
    }
}