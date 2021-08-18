using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] int timeBeforeLosing = 60;

    LevelLoader loader;
    UIUpdater uI;

    private void Awake()
    {
        loader = FindObjectOfType<LevelLoader>();
        uI = FindObjectOfType<UIUpdater>();

        StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        for (int i = timeBeforeLosing; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);

            uI.UpdateTimer(i.ToString());
        }

        loader.LoadLevel(true);
    }
}
