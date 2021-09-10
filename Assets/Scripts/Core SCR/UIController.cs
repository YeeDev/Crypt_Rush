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
        LevelLoader loader;

        private void Awake()
        {
            state = GetComponent<StateHandler>();
            loader = GetComponent<LevelLoader>();
        }

        private void Start() { UnPauseGame(); }

        private void UnPauseGame()
        {
            if (Time.timeScale <= 0)
            {
                paused = true;
                PauseGame();
            }
        }

        private void Update() { if (Input.GetKeyDown(KeyCode.Escape)) { PauseGame(); } }

        //Also called in UI.
        public void PauseGame()
        {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            state.SetState = paused ? GameState.NotPlaying : GameState.Playing;
            pauseMenu.SetActive(paused);
        }

        //Called in UI.
        public void LoadMainMenu() { loader.UILoadLevel(0); }
    }
}