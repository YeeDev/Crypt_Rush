using UnityEngine;

public class Spiker : MonoBehaviour, ITrap
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

    //Called in TrapActivator
    public void ActivateTrap()
    {
        spikeCollider.enabled = true;
        rgb.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        Invoke("PierceGround", pierceWaitTime);    //Pierces the ground at different depths for a more organic look.
    }

    private void PierceGround()
    {
        Destroy(rgb);
        transform.tag = "Untagged";
        spikeCollider.enabled = true;
        spikeCollider.isTrigger = false;
    }
}
