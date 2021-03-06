using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptRush.Core
{
    public class StateHandler : MonoBehaviour
    {
        [SerializeField] GameState currentState = 0;

        //Separated just to be clear as to what is the action taken.
        public GameState GetCurrentState { get => currentState; }  //Used in CollisionHandler and PlayerController.
        public GameState SetState { set => currentState = value; } //Used in CollisionHandler and UIController.

        private void Awake()
        {
            currentState = GameState.NotPlaying;
            StartCoroutine(SetToPlay());
        }

        private IEnumerator SetToPlay()
        {
            yield return new WaitForSeconds(2);
            currentState = GameState.Playing;
        }
    }
}