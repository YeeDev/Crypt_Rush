using System.Collections;
using UnityEngine;

public class BallTeleporter : MonoBehaviour
{
    [SerializeField] bool teleports = true;
    [SerializeField] float timeToRespawn = 5;
    [SerializeField] Transform ballPositioner = null;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.CompareTag("Rolling Ball"))
        {
            colliderObject.SetActive(false);

            if (teleports)
            {
                StartCoroutine(RespawnBall(colliderObject));
            }
        }
    }

    private IEnumerator RespawnBall(GameObject ball)
    {
        yield return new WaitForSeconds(timeToRespawn);

        ball.transform.position = ballPositioner.position;
        ball.SetActive(true);
    }
}
