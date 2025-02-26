using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int Lap => lap;

    int lap = 0;
    int checkpoint = -1;
    int checkpointCount;
    int nextCheckpoint = 0;

    private void Start()
    {
        checkpointCount = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int numer = int.Parse(other.name);
            if ( numer == nextCheckpoint )
            {
                checkpoint = numer;
                nextCheckpoint++;
                nextCheckpoint = nextCheckpoint % checkpointCount;

                if (checkpoint == 0)
                    lap++;
            }
        }
    }
}
