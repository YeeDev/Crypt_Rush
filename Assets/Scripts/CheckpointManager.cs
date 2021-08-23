using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] Transform[] checkpoints = null;

    //TODO set this right
    public void RespawnPlayer(Transform player)
    {
        player.position = checkpoints[0].position;
    }

    public Transform SetCheckpoint(Transform checkpoint)
    {
        foreach (var transform in checkpoints)
        {
            if (checkpoint != transform) { continue; }

            return transform;
        }

        return null;
    }
}
