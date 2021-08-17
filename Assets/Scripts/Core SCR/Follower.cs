using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    //Use an empty object and child the camera to it.

    [SerializeField] float followSmooth = 1;

    Vector3 velocity = Vector3.zero;
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player == null) { return; }

        transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, followSmooth);
    }
}
