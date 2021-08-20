using UnityEngine;
using CryptRush.Core;
using CryptRush.Stats;
using CryptRush.Movement;
using CryptRush.Animation;
using CryptRush.ObstacleManagement;

namespace CryptRush.Collisions
{
    public class CollisionHandler : MonoBehaviour
    {
        bool isInvulnerable;
        PlayerMover mover;
        PlayerAnimator anim;
        StatsHandler stats;
        LevelLoader loader;

        private void Awake()
        {
            stats = GetComponent<StatsHandler>();
            mover = GetComponent<PlayerMover>();
            anim = GetComponent<PlayerAnimator>();
            loader = FindObjectOfType<LevelLoader>();
        }

        private void OnTriggerEnter(Collider other) { CheckCollisionType(other.transform); }
        private void OnCollisionEnter(Collision collision) { CheckCollisionType(collision.transform); }

        private void CheckCollisionType(Transform collisioner)
        {
            if (stats.IsPlayerDead) { return; }

            if (collisioner.CompareTag("Obstacle") || collisioner.CompareTag("Instant Killer")) { TakeDamage(collisioner); }
            if (collisioner.CompareTag("Trap Activator")) { ActivateTrap(collisioner); }
            if (collisioner.CompareTag("Goal")) { StartCoroutine(loader.LoadLevel()); }
        }

        private void TakeDamage(Transform damageDealer)
        {
            if (isInvulnerable) { return; }

            isInvulnerable = true;
            int damageToTake = !transform.CompareTag("Instant Killer") ? -1 : -99999;
            stats.ModifyHealth(damageToTake);

            if (stats.IsPlayerDead)
            {
                //KillPlayer
            }

            anim.TriggerAnimation("TakeDamage");
            mover.Push(damageDealer.position);
        }

        //Called in Animation Event
        public void MakeVulnerable() { isInvulnerable = false; }

        private void ActivateTrap(Transform trap) { StartCoroutine(trap.GetComponent<TrapActivator>().ActivateTrap()); }
    }
}
