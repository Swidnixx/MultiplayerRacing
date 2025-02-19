using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public int laps = 2;

    public int countDownTimer = 3;

    private void Start()
    {
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
}
