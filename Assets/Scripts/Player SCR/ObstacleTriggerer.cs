using UnityEngine;

public class ObstacleTriggerer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap Activator"))
        {
            other.GetComponent<TrapActivator>().StartCoroutine("ActivateTrap");
        }
    }
}
