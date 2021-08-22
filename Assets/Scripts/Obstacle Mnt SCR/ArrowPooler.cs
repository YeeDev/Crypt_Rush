using System.Collections.Generic;
using UnityEngine;
using CryptRush.Obstacle;

namespace CryptRush.ObstacleManagement
{
    public class ArrowPooler : MonoBehaviour
    {
        [SerializeField] int startingArrows = 10;
        [SerializeField] bool canCreateArrows = true;
        [SerializeField] GameObject arrow = null;

        private Queue<GameObject> arrowQueue = new Queue<GameObject>();

        //Used in ArrowShooter
        public Quaternion GetArrowRotation { get => arrow.transform.rotation; }

        private void Awake()
        {
            CreateInitialArrows();
        }

        private void CreateInitialArrows()
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
            newArrow.GetComponent<Arrow>().SetArrowPooler = this;

            return newArrow;
        }

        //Called from Arrowshooter
        public GameObject GetArrow()
        {
            if (arrowQueue.Count > 0) { return arrowQueue.Dequeue(); }

            if (!canCreateArrows) { return null; }

            return CreateNewArrow();
        }

        //Called from Arrower
        public void EnqueueArrow(GameObject arrowToEnqueue)
        {
            arrowToEnqueue.SetActive(false);
            arrowQueue.Enqueue(arrowToEnqueue);
        }
    }
}
