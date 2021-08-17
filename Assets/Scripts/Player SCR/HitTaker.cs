using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class HitTaker : MonoBehaviour
{
    [SerializeField] int hitsBeforeRestarting = 3;
    [SerializeField] string[] damagerTags = null;

    bool isInvulnerable = false;
    UIUpdater uI;
    PlayerAnimator plyAnm;
    LevelLoader loader;
    PlayerMover mover;

    public bool IsAlive() { return hitsBeforeRestarting > 0; }

    private void Awake()
    {
        plyAnm = GetComponent<PlayerAnimator>();
        uI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIUpdater>();
        mover = GetComponent<PlayerMover>();
        loader = FindObjectOfType<LevelLoader>();
    }

    private void Start()
    {
        uI.InitializeUI(hitsBeforeRestarting);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckForCorrectTag(collision.transform.tag) && !isInvulnerable) { CalculateHits(collision.transform.position); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckForCorrectTag(other.tag) && !isInvulnerable) { CalculateHits(other.transform.position); }
    }

    private bool CheckForCorrectTag(string tagToCheck)
    {
        foreach (string tag in damagerTags)
        {
            if (tagToCheck == tag) { return true; }
        }

        return false;
    }

    private void CalculateHits(Vector3 hitterPosition)
    {
        hitsBeforeRestarting--;
        uI.ResizeHeartBar(-1);

        if (hitsBeforeRestarting > 0)
        {
            mover.PushRigidbody(hitterPosition);
            SetVulnerability();
            plyAnm.TriggerAnimation("Invunerable");
            return;
        }

        KillPlayer();
    }

    public void SetVulnerability() { isInvulnerable = !isInvulnerable; }     //Also called in animation event.

    private void KillPlayer()
    {
        StartCoroutine(loader.LoadLevel(true));
        Debug.Log("Wasted!");
        Debug.Log("Kill the player!"); //TODO
    }
}
