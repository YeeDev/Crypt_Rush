using UnityEngine;
using CryptRush.UI;
using CryptRush.Core;

namespace CryptRush.Stats
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField] int health = 3;

        StateHandler state;
        UIUpdater uI;

        private void Awake()
        {
            state = FindObjectOfType<StateHandler>();
            uI = FindObjectOfType<UIUpdater>();
        }

        private void Start() { uI.InitializeUI(health); } //Avoids racing.

        //Called in CollisionHandler
        public void ModifyHealth(int quantity)
        {
            health += quantity;
            uI.ResizeHeartBar(quantity);

            if (health <= 0) { state.SetState = GameState.Dead; }
        }
    }
}
