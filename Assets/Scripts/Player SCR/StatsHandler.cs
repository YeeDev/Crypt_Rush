using UnityEngine;
using CryptRush.UI;

namespace CryptRush.Stats
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField] int health = 3;

        UIUpdater uI;

        public bool IsPlayerDead { get => health <= 0; }

        private void Awake() { uI = FindObjectOfType<UIUpdater>(); }
        private void Start() { uI.InitializeUI(health); } //Avoids racing.

        //Called in CollisionHandler
        public void ModifyHealth(int quantity)
        {
            health += quantity;
            uI.ResizeHeartBar(quantity);
        }
    }
}
