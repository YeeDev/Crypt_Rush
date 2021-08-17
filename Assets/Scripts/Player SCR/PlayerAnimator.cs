using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    //TODO Check if this script is necessary
    Animator anm;

    private void Awake()
    {
        anm = GetComponent<Animator>();
    }

    public void TriggerAnimation(string animation) { anm.SetTrigger(animation); }
}
