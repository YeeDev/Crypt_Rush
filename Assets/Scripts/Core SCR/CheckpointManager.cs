using UnityEngine;

namespace CryptRush.Core
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] Transform initialCheckPoint = null;

        Transform currentCheckpoint;

        public Vector3 GetCheckpoint { get => currentCheckpoint.position; }

        private void Awake() { currentCheckpoint = initialCheckPoint; }

        public void SetCheckpoint(Transform checkpoint) { currentCheckpoint = checkpoint; }
    }
}