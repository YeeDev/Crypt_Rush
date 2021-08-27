using System.Collections;
using UnityEngine;
using CryptRush.Core;
using CryptRush.Stats;
using CryptRush.Movement;
using CryptRush.Animation;
using CryptRush.ObstacleManagement;
using CryptRush.Control;

namespace CryptRush.Collisions
{
    public class CollisionHandler : MonoBehaviour
    {
        bool isInvulnerable;
        PlayerMover mover;
        PlayerAnimator anim;
        StatsHandler stats;
        LevelLoader loader;
        CheckpointManager checkpoint;
        StateHandler state;
        UIController uI;

        private void Awake()
        {
            stats = GetComponent<StatsHandler>();
            mover = GetComponent<PlayerMover>();
            anim = GetComponent<PlayerAnimator>();
            loader = FindObjectOfType<LevelLoader>();
            checkpoint = FindObjectOfType<CheckpointManager>();
            state = FindObjectOfType<StateHandler>();
            uI = FindObjectOfType<UIController>();
        }

        private void OnTriggerEnter(Collider other) { CheckCollisionType(other.transform); }
        private void OnCollisionEnter(Collision collision) { CheckCollisionType(collision.transform); }

        private void CheckCollisionType(Transform collisioner)
        {
            if (state.GetCurrentState != GameState.Playing) { return; }

            if (collisioner.CompareTag("Obstacle") || collisioner.CompareTag("Instant Killer")) { TakeDamage(collisioner); }
            if (collisioner.CompareTag("Trap Activator")) { ActivateTrap(collisioner); }
            if (collisioner.CompareTag("Goal")) { StartCoroutine(ProcessGoal(collisioner)); }
            if (collisioner.CompareTag("Fall")) { ProceessFall(collisioner); }
            if (collisioner.CompareTag("Checkpoint")) { checkpoint.SetCheckpoint(collisioner);  }
        }

        private void TakeDamage(Transform damageDealer) //TODO Should this be handled somewhere else?
        {
            if (isInvulnerable && !damageDealer.CompareTag("Fall")) { return; }

            isInvulnerable = true;
            int damageToTake = !damageDealer.CompareTag("Instant Killer") ? -1 : -99999;
            stats.ModifyHealth(damageToTake);

            if (state.GetCurrentState != GameState.Playing)
            {
                loader.StarLoadWithDelay();
                anim.DeadAnimation();
                return;
            }

            anim.TriggerAnimation("TakeDamage");
            if (!damageDealer.CompareTag("Fall")) { mover.Push(damageDealer.position); }
        }

        //Called in Animation Event
        public void MakeVulnerable() { isInvulnerable = false; }

        private void ActivateTrap(Transform trap) { StartCoroutine(trap.GetComponent<TrapActivator>().ActivateTrap()); }

        private IEnumerator ProcessGoal(Transform goal)
        {
            state.SetState = GameState.NotPlaying;

            while (mover.MovingToPoint(goal.position)) { yield return new WaitForFixedUpdate(); }

            goal.GetComponentInParent<Animator>().SetTrigger("MoveDown");
            transform.parent = goal;

            loader.StarLoadWithDelay(true);
            uI.GetComponent<Animator>().SetTrigger("FadeOut");
        }

        private void ProceessFall(Transform collisioner)
        {
            TakeDamage(collisioner);

            if (state.GetCurrentState != GameState.Playing) { return; }

            mover.Respawn(checkpoint.GetCheckpoint);
        }
    }
}
