using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiker : MonoBehaviour
{
    [SerializeField] float minPierceValue = 0.1f;
    [SerializeField] float maxPierceValue = 0.5f;
    [SerializeField] Collider spikeCollider = null;

    float pierceWaitTime;
    Rigidbody rgb;

    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();

        pierceWaitTime = Random.Range(minPierceValue, maxPierceValue);
    }

    //Called in SpikeDropper
    public void ActivateSpike()
    {
        spikeCollider.enabled = true;
        rgb.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        Invoke("PierceGround", pierceWaitTime);
    }

    private void PierceGround()
    {
        Destroy(rgb);
        transform.tag = "Untagged";
        spikeCollider.enabled = true;
        spikeCollider.isTrigger = false;
    }
}
