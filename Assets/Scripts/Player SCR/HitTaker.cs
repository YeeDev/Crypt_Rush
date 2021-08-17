using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class HitTaker : MonoBehaviour
{
    [SerializeField] int hitsBeforeRestarting = 3;
    [SerializeField] float pushForce = 10;
    [SerializeField] string[] damagerTags = null;

    bool isInvulnerable = false;
    Rigidbody rgb;
    PlayerAnimator plyAnm;
    UIUpdater uI;

    public bool IsAlive() { return hitsBeforeRestarting > 0; }

    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();
        plyAnm = GetComponent<PlayerAnimator>();
        uI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIUpdater>();
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
            Push(hitterPosition);
            SetVulnerability();
            plyAnm.TriggerAnimation("Invunerable");
            return;
        }

        KillPlayer();
    }

    private void Push(Vector3 hitterPosition)
    {
        hitterPosition.y = transform.position.y;
        rgb.AddForce((transform.position - hitterPosition).normalized * pushForce, ForceMode.VelocityChange);
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
