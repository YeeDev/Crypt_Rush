using UnityEngine;

namespace CryptRush.Obstacle
{
    public class AnimatedTrap : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] float animationSpeed = 1;

        Animator anm;

        private void Awake()
        {
            anm = GetComponent<Animator>();
            anm.speed = animationSpeed;
        }
    }
}