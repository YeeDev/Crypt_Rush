using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    Animator anm;

    private void Awake()
    {
        anm = GetComponent<Animator>();
    }

    public void TriggerAnimation(string animation) { anm.SetTrigger(animation); }
}
