using UnityEngine;

public class RollingBaller : MonoBehaviour, ITrap
{
    [SerializeField] float speed;
    [SerializeField] Vector3 moveDirection;

    Vector3 directionalSpeed;
    Rigidbody rgb;

    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();
    }

    public void ActivateTrap()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        RollBall();
    }

    private void RollBall()
    {
        directionalSpeed = moveDirection.normalized * speed;
        directionalSpeed.y = rgb.velocity.y;
        rgb.velocity = directionalSpeed;
    }
}
