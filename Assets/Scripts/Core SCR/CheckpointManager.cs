using UnityEngine;

namespace CryptRush.Core
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] Transform initialCheckPoint = null;
        [SerializeField] Animator flagAnimator = null;
        [SerializeField] ParticleSystem flagParticles = null;

        bool alreadySet;
        Transform currentCheckpoint;

        //Used in CollisionHandler
        public Vector3 GetCheckpoint { get => currentCheckpoint.position; }

        private void Awake() { currentCheckpoint = initialCheckPoint; }

        //Called in CollisionHandler
        public void SetCheckpoint(Transform checkpoint)
        {
            if(alreadySet) { return; }

            alreadySet = true;
            currentCheckpoint = checkpoint;
            flagAnimator.SetTrigger("GetCheckpoint");
            flagParticles.Play();
        }
    }
}