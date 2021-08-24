using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptRush.Core
{
    public class StateHandler : MonoBehaviour
    {
        [SerializeField] GameState currentState = 0;

        //Separated just to be clear as to what is the action taken.
        public GameState GetCurrentState { get => currentState; }
        public GameState SetState { set => currentState = value; }
    }
}