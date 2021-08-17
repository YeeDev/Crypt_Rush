using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriggerer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            other.GetComponent<SpikeDropper>().StartCoroutine("DropSpike");
        }
    }
}
