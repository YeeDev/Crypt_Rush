using System.Collections;
using UnityEngine;
using CryptRush.Core;
using CryptRush.Stats;
using CryptRush.Movement;
using CryptRush.Animation;
using CryptRush.ObstacleManagement;
using CryptRush.Control;
using CryptRush.Sound;

namespace CryptRush.Collisions
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] AudioClip takeDamageAudioClip;

        bool isInvulnerable;
        PlayerMover mover;
        PlayerAnimator anim;
        StatsHandler stats;
        LevelLoader loader;
        CheckpointManager checkpoint;
        StateHandler state;
        UIController uI;
        SFXPlayer sFXPlayer;

        private void Awake()
        {
            stats = GetComponent<StatsHandler>();
            mover = GetComponent<PlayerMover>();
            anim = GetComponent<PlayerAnimator>();
            sFXPlayer = GetComponent<SFXPlayer>();
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

        private void TakeDamage(Transform damageDealer)
        {
            if (isInvulnerable && !damageDealer.CompareTag("Fall")) { return; }

            isInvulnerable = true;
            int damageToTake = !damageDealer.CompareTag("Instant Killer") ? -1 : -99999;
            stats.ModifyHealth(damageToTake);

            if (state.GetCurrentState != GameState.Playing)
            {
                loader.StarLoadWithDelay(4);
                anim.DeadAnimation();
                uI.GetComponent<Animator>().SetTrigger("FadeOutDelay");
                return;
            }

            anim.TriggerAnimation("TakeDamage");
            Invoke("MakeVulnerable", 1.5f);
            if (!damageDealer.CompareTag("Fall"))
            {
                sFXPlayer.PlayClip(takeDamageAudioClip);
                mover.Push(damageDealer.position);
            }
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

            anim.SetBoolAnimation("IsRunning", false);
            loader.StarLoadWithDelay(2, true);
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
