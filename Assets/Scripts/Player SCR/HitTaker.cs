using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class HitTaker : MonoBehaviour
{
    [SerializeField] int hitsBeforeRestarting = 3;
    [SerializeField] string[] damagerTags = null;

    bool isInvulnerable = false;
    PlayerAnimator plyAnm;

    public bool IsAlive() { return hitsBeforeRestarting > 0; }

    private void Awake()
    {
        plyAnm = GetComponent<PlayerAnimator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckForCorrectTag(collision.transform.tag) && !isInvulnerable) { CalculateHits(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckForCorrectTag(other.tag) && !isInvulnerable) { CalculateHits(); }
    }

    private bool CheckForCorrectTag(string tagToCheck)
    {
        foreach (string tag in damagerTags)
        {
            if (tagToCheck == tag) { return true; }
        }

        return false;
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
