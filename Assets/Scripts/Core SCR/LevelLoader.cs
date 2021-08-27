using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO This needs a refactor
//TODO This won't work like this at all.
namespace CryptRush.Core
{
    public class LevelLoader : MonoBehaviour
    {
        int currentLevelIndex;

        private void Awake() { currentLevelIndex = SceneManager.GetActiveScene().buildIndex; }

        //Called in UI
        public void UILoadLevel(int otherLevel) { LoadLevel(false, otherLevel); }

        //Called in 
        public void StarLoadWithDelay(int timeToLoad = 2, bool loadNext = false, int loadOther = -1)
        {
            StartCoroutine(DelayedLoad(timeToLoad, loadNext, loadOther));
        }

        private void LoadLevel(bool loadNext, int loadOther) { SceneManager.LoadScene(GetLevelToLoad(loadNext, loadOther)); }

        private IEnumerator DelayedLoad(int timeToLoad = 2, bool loadNext = false, int loadOther = -1)
        {
            yield return new WaitForSeconds(timeToLoad);

            LoadLevel(loadNext, loadOther);
        }

        private int GetLevelToLoad(bool loadNext, int loadOther)
        {
            int levelToLoad = currentLevelIndex;
            levelToLoad = loadNext ? (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings : levelToLoad;
            levelToLoad = loadOther != -1 ? loadOther : levelToLoad;
            return levelToLoad;
        }
    }
}
