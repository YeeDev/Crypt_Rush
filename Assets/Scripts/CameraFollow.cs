using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float followSmooth = 1;

    Vector3 velocity = Vector3.zero;
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, followSmooth);
    }
}
