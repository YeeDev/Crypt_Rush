using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDropper : MonoBehaviour
{
    [SerializeField] float fallWaitTime = 1;

    Spiker spike;
    Collider triggerCollider;

    private void Awake()
    {
        spike = GetComponentInChildren<Spiker>();
        triggerCollider = GetComponent<Collider>();
    }

    public IEnumerator DropSpike()
    {
        triggerCollider.enabled = false;

        yield return new WaitForSeconds(fallWaitTime);

        spike.ActivateSpike();
    }
}
