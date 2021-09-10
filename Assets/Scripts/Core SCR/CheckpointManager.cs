using UnityEngine;

namespace CryptRush.Core
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] Transform initialCheckPoint = null;

        Transform currentCheckpoint;

        //Used in CollisionHandler
        public Vector3 GetCheckpoint { get => currentCheckpoint.position; }

        private void Awake() { currentCheckpoint = initialCheckPoint; }

        //Called in CollisionHandler
        public void SetCheckpoint(Transform checkpoint)
        {
            currentCheckpoint = checkpoint;
            checkpoint.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}