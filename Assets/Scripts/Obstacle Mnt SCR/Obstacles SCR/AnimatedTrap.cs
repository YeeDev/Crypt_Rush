using UnityEngine;

namespace CryptRush.Obstacle
{
    public class AnimatedTrap : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] float animationSpeed = 1;
        [Range(0, 1)] [SerializeField] float animationOffset = 0;
        [Range(-1, 1)] [SerializeField] int rotateDirection = 1;

        Animator anm;

        private void Awake()
        {
            anm = GetComponent<Animator>();

            SetAnimationParameters();
        }

        private void SetAnimationParameters()
        {
            anm.speed = animationSpeed;
            anm.SetFloat("Offset", animationOffset);
            anm.SetFloat("Direction", rotateDirection);
        }
    }
}