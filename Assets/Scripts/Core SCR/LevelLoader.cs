using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelay = 2;
    [SerializeField] int menuSceneIndex = 0;

    int currentLevelIndex;

    private void Awake() { currentLevelIndex = SceneManager.GetActiveScene().buildIndex; }

    //Called in HitTaker, Obstacle Triggerer
    public IEnumerator LoadLevel(bool loadCurrent = false, int loadOther = 0)
    {
        yield return new WaitForSeconds(loadDelay);

        SceneManager.LoadScene(GetLevelToLoad(loadCurrent, loadOther));
    }

    private int GetLevelToLoad(bool loadCurrent, int loadOther)
    {
        int levelToLoad = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        levelToLoad = loadCurrent ? currentLevelIndex : levelToLoad;
        levelToLoad = loadOther != 0 ? loadOther : levelToLoad;

        //TODO Load Main Menu when finishing?

        return levelToLoad;
    }
}
