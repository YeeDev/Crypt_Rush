using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class HitTaker : MonoBehaviour
{
    [SerializeField] int hitsBeforeRestarting = 3;

    bool isInvulnerable = false;
    PlayerAnimator plyAnm;

    public bool IsAlive() { return hitsBeforeRestarting > 0; }

    private void Awake()
    {
        plyAnm = GetComponent<PlayerAnimator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle") && !isInvulnerable)
        {
            collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            CalculateHits();
        }
    }

    private void CalculateHits()
    {
        hitsBeforeRestarting--;

        if (hitsBeforeRestarting > 0)
        {
            SetVulnerability();
            plyAnm.TriggerAnimation("Invunerable");
            return;
        }

        KillPlayer();
    }

    //Also called in animation event.
    public void SetVulnerability()
    {
        isInvulnerable = !isInvulnerable;
    }

    private void KillPlayer()
    {
        Debug.Log("Wasted!");
        Debug.Log("Kill the player!"); //TODO
    }
}
