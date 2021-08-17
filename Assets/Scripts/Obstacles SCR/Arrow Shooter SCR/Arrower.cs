using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrower : MonoBehaviour
{
    [SerializeField] float timeToExpire = 5;

    ArrowPooler pooler;

    public ArrowPooler SetArrowPooler { set => pooler = value; }

    private void OnEnable() { StartCoroutine(ExpireArrow()); }

    private void OnTriggerEnter(Collider other) { ReEnqueueArrow(); }

    private IEnumerator ExpireArrow()
    {
        if (timeToExpire <= Mathf.Epsilon) { yield break; } 

        yield return new WaitForSeconds(timeToExpire);

        ReEnqueueArrow();
    }

    private void ReEnqueueArrow() { pooler.EnqueueArrow(gameObject); }
}
