using UnityEngine;
using CryptRush.UI;

namespace CryptRush.Core
{
    public class Scorer : MonoBehaviour
    {
        [SerializeField] int pointsPerSecond = 10;
        [SerializeField] int pointsPerHeart = 50;

        int score;
        UIUpdater uI;

        private void Awake() { uI = FindObjectOfType<UIUpdater>(); }

        //Called in CollisionHandler
        public void UpdateScore(int lastingTime, int hearts)
        {
            score += (lastingTime * pointsPerSecond) + (hearts * pointsPerHeart);
            uI.UpdateScore(score);
        }
    }
}