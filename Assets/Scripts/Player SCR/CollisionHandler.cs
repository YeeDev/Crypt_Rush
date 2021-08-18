using UnityEngine;
using CryptRush.Stats;
using CryptRush.Movement;
using CryptRush.Animation;

namespace CryptRush.Collisions
{
    public class CollisionHandler : MonoBehaviour
    {
        bool isInvulnerable;
        PlayerMover mover;
        PlayerAnimator anim;
        StatsHandler stats;

        private void Awake()
        {
            stats = GetComponent<StatsHandler>();
            mover = GetComponent<PlayerMover>();
            anim = GetComponent<PlayerAnimator>();
        }

        private void OnTriggerEnter(Collider other) { CheckCollisionType(other.transform); }
        private void OnCollisionEnter(Collision collision) { CheckCollisionType(collision.transform); }

        private void CheckCollisionType(Transform collisioner)
        {
            if (collisioner.CompareTag("Obstacle")) { TakeDamage(collisioner); }
        }

        private void TakeDamage(Transform damageDealer)
        {
            if (isInvulnerable) { return; }

            isInvulnerable = true;
            stats.ModifyHealth(-1);

            if (stats.IsPlayerDead)
            {
                //KillPlayer
            }

            anim.TriggerAnimation("TakeDamage");
            mover.Push(damageDealer.position);
        }

        //Called in Animation Event
        public void MakeVulnerable() { isInvulnerable = false; } 
    }
}
