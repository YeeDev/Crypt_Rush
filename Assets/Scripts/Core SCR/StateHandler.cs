using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptRush.Core
{
    public class StateHandler : MonoBehaviour
    {
        [SerializeField] GameState currentState = 0;

        public GameState GetCurrentState { get => currentState; }
        public GameState SetState { set => currentState = value; }
    }
}