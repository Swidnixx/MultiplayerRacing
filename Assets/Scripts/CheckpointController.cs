using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Transform LastCheckpoint => lastCheckpoint;
    public int Lap => lap;

    public int lap = 0;
    public int checkpoint = -1;

    int checkpointCount;
    int nextCheckpoint = 0;

    Transform lastCheckpoint;

    private void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointCount = checkpoints.Length;
        foreach(var ckp in checkpoints)
        {
            if(ckp.name == "0")
            {
                lastCheckpoint = ckp.transform;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int numer = int.Parse(other.name);
            if ( numer == nextCheckpoint )
            {
                checkpoint = numer;
                lastCheckpoint = other.transform;
                nextCheckpoint++;
                nextCheckpoint = nextCheckpoint % checkpointCount;

                if (checkpoint == 0)
                    lap++;
            }
        }
    }
}
