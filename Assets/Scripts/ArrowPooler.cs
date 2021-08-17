using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPooler : MonoBehaviour
{
    [SerializeField] int startingArrows = 10;
    [SerializeField] bool canCreateArrows = true;
    [SerializeField] GameObject arrow = null;

    private Queue<GameObject> arrowQueue = new Queue<GameObject>();

    public Quaternion GetArrowRotation { get => arrow.transform.rotation; }

    public void Awake()
    {
        for (int i = 0; i < startingArrows; i++)
        {
            arrowQueue.Enqueue(CreateNewArrow());
        }
    }

    private GameObject CreateNewArrow()
    {
        GameObject newArrow = Instantiate(arrow, transform);
        newArrow.SetActive(false);
        newArrow.GetComponent<Arrower>().SetArrowPooler = this;

        return newArrow;
    }

    public GameObject GetArrow()
    {
        if (arrowQueue.Count > 0) { return arrowQueue.Dequeue(); }

        if (!canCreateArrows) { return null; }

        return CreateNewArrow();
    }

    public void EnqueueArrow(GameObject arrowToEnqueue)
    {
        arrowToEnqueue.SetActive(false);
        arrowQueue.Enqueue(arrowToEnqueue);
    }
}