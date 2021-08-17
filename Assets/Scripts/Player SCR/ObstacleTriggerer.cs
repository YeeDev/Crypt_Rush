using UnityEngine;

public class ObstacleTriggerer : MonoBehaviour
{
    LevelLoader loader;

    private void Awake()
    {
        loader = FindObjectOfType<LevelLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ActivateTrap(other);
        WinGame(other);
    }

    private static void ActivateTrap(Collider other)
    {
        if (other.CompareTag("Trap Activator"))
        {
            other.GetComponent<TrapActivator>().StartCoroutine("ActivateTrap");
        }
    }

    private void WinGame(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            StartCoroutine(loader.LoadLevel());
        }
    }
}
