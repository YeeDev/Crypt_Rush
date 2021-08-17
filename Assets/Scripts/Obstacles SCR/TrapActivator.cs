using System.Collections;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    [SerializeField] GameObject trap = null;
    [SerializeField] float activationDelay = 1;

    ITrap trapToActivate;
    Collider triggerCollider;

    private void Awake()
    {
        trapToActivate = trap.GetComponent<ITrap>();
        triggerCollider = GetComponent<Collider>();
    }

    public IEnumerator ActivateTrap()
    {
        triggerCollider.enabled = false;

        yield return new WaitForSeconds(activationDelay);

        trapToActivate.ActivateTrap();
    }
}
