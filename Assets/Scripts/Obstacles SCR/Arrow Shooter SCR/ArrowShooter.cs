using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] bool shoots = true;
    [SerializeField] float fireRate = 2;
    [SerializeField] float shootDelay = 0;
    [SerializeField] float arrowSpeed = 2;

    Quaternion targetRotation;
    ArrowPooler pooler;

    public bool Shoots { set => shoots = value; }

    private void Awake()
    {
        pooler = FindObjectOfType<ArrowPooler>();
        targetRotation = transform.rotation * pooler.GetArrowRotation;
    }

    private void Start()
    {
        StartCoroutine(ShootRepeteadly());
    }

    public IEnumerator ShootRepeteadly()
    {
        yield return new WaitForSeconds(shootDelay);

        while (shoots)
        {
            GameObject arrow = pooler.GetArrow();

            if (arrow == null)
            {
                Debug.LogWarning("No Arrows Available");
                yield return new WaitForSeconds(fireRate);
                continue; //The "continue" function stops the loop at the point it is without stopping the loop.
            }

            InitializeArrow(arrow);

            yield return new WaitForSeconds(fireRate);
        }
    }

    private void InitializeArrow(GameObject arrow)
    {
        arrow.transform.position = transform.position;
        arrow.transform.rotation = targetRotation;
        arrow.SetActive(true);

        arrow.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;
    }
}
