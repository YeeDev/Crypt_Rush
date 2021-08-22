using UnityEngine;
using UnityEngine.UI;

namespace CryptRush.UI
{
    public class UIUpdater : MonoBehaviour
    {
        [SerializeField] RectTransform currentHearts = null;
        [SerializeField] RectTransform backgroundHearts = null;
        [SerializeField] Text timerText = null;

        float singleHeartSize;
        float maxSize;

        private void Awake() { singleHeartSize = currentHearts.sizeDelta.x; }

        //Called in StatsHandler.
        public void InitializeUI(float maxHearts)
        {
            maxSize = maxHearts * singleHeartSize;

            ResizeHeartBar(maxHearts, true);
        }

        //Also called in StatsHandler.
        public void ResizeHeartBar(float modifier, bool changeBackground = false)
        {
            Vector2 newDeltaSize = currentHearts.sizeDelta;
            newDeltaSize.x = Mathf.Clamp(newDeltaSize.x + modifier * singleHeartSize, 0, maxSize);
            currentHearts.sizeDelta = newDeltaSize;

            if (changeBackground)
            {
                backgroundHearts.sizeDelta = newDeltaSize;
            }
        }

        //Called in Timer.
        public void UpdateTimer(string timeToDisplay) { timerText.text = $"{timeToDisplay} sec"; }
    }
}
