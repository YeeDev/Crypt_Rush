using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTrap : MonoBehaviour
{
    [Range(0, 10)][SerializeField] float animationSpeed = 1;

    Animator anm;

    private void Awake()
    {
        anm = GetComponent<Animator>();
        anm.speed = animationSpeed;
    }
}
