using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool RacePending => racePending;
    static bool racePending = false;

    [SerializeField] int laps = 2;
    [SerializeField] int countDownTimer = 3;

    CheckpointController[] controllers;

    private void Start()
    {
        controllers = FindObjectsOfType<CheckpointController>();
        InvokeRepeating(nameof(CountDown), 3, 1);
    }

    void CountDown()
    {        
        Debug.Log(countDownTimer);
        if (countDownTimer < 1)
        {
            Debug.Log("Start!");
            racePending = true;
            CancelInvoke(nameof(CountDown));
            return;
        }
        countDownTimer--;
    }

    private void Update()
    {
        int finishers = 0;
        foreach(var c in controllers)
        {
            if(c.Lap == laps + 1)
                finishers++;
        }

        if (finishers >= controllers.Length)
        {
            Debug.Log("Race finished");
            racePending = false;
        }
    }
}
